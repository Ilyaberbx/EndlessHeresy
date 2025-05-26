using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Scopes.Gameplay.Services.StaticData;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.Attributes
{
    public sealed class AttributesComponent : PocoComponent, IAttributesReadOnly
    {
        public IReadOnlyReactiveCollection<Attribute> AttributesReadOnly => _attributes;

        private IGameplayStaticDataService _gameplayStaticDataService;

        private StatModifiersComponent _statModifiersComponent;

        private IReactiveCollection<Attribute> _attributes;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _attributes = new ReactiveCollection<Attribute>();
            return Task.CompletedTask;
        }

        public AttributeData[] GetSnapshot()
        {
            return AttributesReadOnly
                .Select(attribute => attribute.GetSnapshot())
                .ToArray();
        }

        public void Increase(AttributeType identifier, int count)
        {
            if (count <= 0)
            {
                return;
            }

            for (var i = 0; i < count; i++)
            {
                IncreaseOne(identifier);
            }
        }

        public void Decrease(AttributeType identifier, int count)
        {
            if (count <= 0)
            {
                return;
            }

            for (var i = 0; i < count; i++)
            {
                DecreaseOne(identifier);
            }
        }

        private void IncreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);
            var existingAttribute =
                _attributes.SingleOrDefault(attribute => attribute.IdentifierProperty.Value == identifier);

            if (existingAttribute == null)
            {
                var defaultData = new AttributeData()
                {
                    Identifier = identifier,
                    Value = configuration.MinValue.Value
                };

                _attributes.Add(new Attribute(defaultData, configuration));
                ProcessModifiers(configuration);
                return;
            }

            existingAttribute.Increase();
            ProcessModifiers(configuration);
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);

            var existingAttribute =
                _attributes.SingleOrDefault(attribute => attribute.IdentifierProperty.Value == identifier);

            if (existingAttribute == null)
            {
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            if (!existingAttribute.TryDecrease())
            {
                return;
            }

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Reverse(modifier);
            }
        }

        private void ProcessModifiers(AttributeItemData data)
        {
            var modifiers = data.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Process(modifier);
            }
        }
    }
}
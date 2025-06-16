using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Stats;
using UniRx;

namespace EndlessHeresy.Runtime.Attributes
{
    public sealed class AttributesComponent : PocoComponent, IAttributesReadOnly
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private readonly AttributeData[] _initialAttributesData;
        private readonly IReactiveCollection<Attribute> _attributes;

        private StatsComponent _statsComponent;
        public IReadOnlyReactiveCollection<Attribute> AttributesReadOnly => _attributes;

        public AttributesComponent(IGameplayStaticDataService gameplayStaticDataService,
            AttributeData[] initialAttributesData)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _initialAttributesData = initialAttributesData;
            _attributes = new ReactiveCollection<Attribute>();
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();

            foreach (var data in _initialAttributesData)
            {
                Increase(data.Identifier, data.Value);
            }

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
            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Identifier == identifier);

            if (existingAttribute == null)
            {
                existingAttribute = configuration.GetAttribute();
                _attributes.Add(existingAttribute);
            }

            var source = existingAttribute.Increase();

            foreach (var modifierData in configuration.Modifiers)
            {
                var statIdentifier = modifierData.StatIdentifier;
                var modifier = modifierData.GetStatModifier(source);
                var stat = _statsComponent.GetStat(statIdentifier);
                stat.AddModifier(modifier);
            }
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Identifier == identifier);

            if (existingAttribute == null)
            {
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            if (!existingAttribute.TryDecrease(out var source))
            {
                DebugUtility.LogException<Exception>($"Attribute reached minimal value: {identifier}");
                return;
            }

            _statsComponent.RemoveAllModifiersBySource(source);
        }
    }
}
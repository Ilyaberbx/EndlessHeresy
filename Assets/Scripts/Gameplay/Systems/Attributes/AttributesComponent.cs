using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Persistant;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using UniRx;
using VContainer;

namespace EndlessHeresy.Gameplay.Attributes
{
    public sealed class AttributesComponent : PocoComponent, IAttributesReadOnly
    {
        public IReadOnlyReactiveCollection<AttributeModel> AttributesReadOnly => _attributes;

        private IGameplayStaticDataService _gameplayStaticDataService;

        private StatModifiersComponent _statModifiersComponent;
        private ReactiveCollection<AttributeModel> _attributes;

        public void SetAttributes(IReadOnlyList<AddAttributeData> data)
        {
            _initialAttributes = data;
        }

        private IReadOnlyList<AddAttributeData> _initialAttributes;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _attributes = new ReactiveCollection<AttributeModel>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statModifiersComponent = Owner.GetComponent<StatModifiersComponent>();

            foreach (var initialAttribute in _initialAttributes)
            {
                Increase(initialAttribute.Identifier, initialAttribute.Value);
            }

            return Task.CompletedTask;
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
            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Identifier.Value == identifier);

            if (existingAttribute == null)
            {
                _attributes.Add(new AttributeModel(identifier, configuration.MinValue));
                ProcessModifiers(configuration);
                return;
            }

            existingAttribute.ValueProperty.Value++;
            ProcessModifiers(configuration);
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);

            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Identifier.Value == identifier);

            if (existingAttribute == null)
            {
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            var minValue = configuration.MinValue;

            if (existingAttribute.ValueProperty.Value <= minValue)
            {
                return;
            }

            existingAttribute.ValueProperty.Value--;

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Reverse(modifier);
            }
        }

        private void ProcessModifiers(AttributeData data)
        {
            var modifiers = data.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Process(modifier);
            }
        }
    }
}
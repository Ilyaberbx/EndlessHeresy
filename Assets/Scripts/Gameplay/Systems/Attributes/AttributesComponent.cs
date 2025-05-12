using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Persistant;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using VContainer;

namespace EndlessHeresy.Gameplay.Attributes
{
    public sealed class AttributesComponent : PocoComponent, IAttributesReadOnly
    {
        private IGameplayStaticDataService _gameplayStaticDataService;
        private StatModifiersComponent _statModifiersComponent;
        private IReadOnlyList<AddAttributeData> _initialAttributes;
        private List<ReactiveProperty<AttributeData>> _attributes;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _attributes = new List<ReactiveProperty<AttributeData>>();
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

        public IReadOnlyList<ReactiveProperty<AttributeData>> GetAll()
        {
            return _attributes.AsReadOnly();
        }

        public void SetAttributes(IReadOnlyList<AddAttributeData> data)
        {
            _initialAttributes = data;
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

            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Value.Identifier == identifier);

            if (existingAttribute == null)
            {
                _attributes.Add(GetNewAttributeProperty(identifier, configuration));
                ProcessModifiers(configuration);
                return;
            }

            existingAttribute.Value.Value++;

            ProcessModifiers(configuration);
        }

        private ReactiveProperty<AttributeData> GetNewAttributeProperty(AttributeType identifier,
            AttributeConfigurationData configuration)
        {
            return new ReactiveProperty<AttributeData>(new AttributeData(identifier, configuration.MinValue));
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);

            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Value.Identifier == identifier);

            if (existingAttribute == null)
            {
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            var minValue = configuration.MinValue;

            if (existingAttribute.Value.Value <= minValue)
            {
                return;
            }

            existingAttribute.Value.Value--;

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Reverse(modifier);
            }
        }

        private void ProcessModifiers(AttributeConfigurationData data)
        {
            var modifiers = data.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Process(modifier);
            }
        }
    }
}
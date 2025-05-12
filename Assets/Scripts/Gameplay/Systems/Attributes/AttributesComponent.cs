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
using VContainer;

namespace EndlessHeresy.Gameplay.Attributes
{
    public sealed class AttributesComponent : PocoComponent, IAttributesReadOnly
    {
        private IGameplayStaticDataService _gameplayStaticDataService;
        private StatModifiersComponent _statModifiersComponent;
        private IReadOnlyList<AttributeData> _attributes;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statModifiersComponent = new StatModifiersComponent();
            return Task.CompletedTask;
        }

        public void Setup(IReadOnlyList<AttributeData> attributes) => _attributes = attributes;

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
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            existingAttribute.Value++;

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Process(modifier);
            }
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);

            var existingAttribute = _attributes.SingleOrDefault(attribute => attribute.Identifier == identifier);

            if (existingAttribute == null)
            {
                DebugUtility.LogException<Exception>($"Can not find: {identifier}");
                return;
            }

            var minValue = configuration.MinValue;

            if (existingAttribute.Value <= minValue)
            {
                return;
            }

            existingAttribute.Value--;

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Reverse(modifier);
            }
        }

        public IReadOnlyList<AttributeData> GetAll()
        {
            return _attributes;
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using VContainer;

namespace EndlessHeresy.Gameplay.Attributes
{
    public sealed class AttributesComponent : PocoComponent
    {
        private IGameplayStaticDataService _gameplayStaticDataService;
        private Dictionary<AttributeType, int> _map;
        private StatModifiersComponent _statModifiersComponent;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _map = new Dictionary<AttributeType, int>();
            _statModifiersComponent = new StatModifiersComponent();
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

            if (!_map.TryGetValue(identifier, out var attribute))
            {
                return;
            }

            _map[identifier] = ++attribute;
            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Process(modifier);
            }
        }

        private void DecreaseOne(AttributeType identifier)
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(identifier);

            if (!_map.TryGetValue(identifier, out var attribute))
            {
                return;
            }

            var minValue = configuration.MinValue;

            if (attribute <= minValue)
            {
                return;
            }

            _map[identifier] = --attribute;

            var modifiers = configuration.Modifiers;

            foreach (var modifier in modifiers)
            {
                _statModifiersComponent.Reverse(modifier);
            }
        }
    }
}
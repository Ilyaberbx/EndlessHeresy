using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.UI.Huds.Abilities;
using EndlessHeresy.UI.Huds.Stats;
using EndlessHeresy.UI.Services.Huds;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IStateMachineContext
    {
        private IHudsService _hudsService;

        private HealthComponent _healthComponent;
        private AbilitiesStorageComponent _abilitiesStorage;
        private StatsComponent _statsComponent;

        [Inject]
        public void Construct(IHudsService hudsService) => _hudsService = hudsService;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _healthComponent = GetComponent<HealthComponent>();
            _abilitiesStorage = GetComponent<AbilitiesStorageComponent>();
            _statsComponent = GetComponent<StatsComponent>();

            await Task.WhenAll(ShowAbilitiesHudAsync(), ShowStatsHudAsync());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(10);
            }
        }

        private Task ShowAbilitiesHudAsync()
        {
            var model = new AbilitiesHudModel(_abilitiesStorage.Abilities);
            return _hudsService.ShowAsync<AbilitiesHudController, AbilitiesHudModel>(model, ShowType.Additive);
        }

        private Task ShowStatsHudAsync()
        {
            var model = new StatsHudModel(_statsComponent);
            return _hudsService.ShowAsync<StatsHudController, StatsHudModel>(model, ShowType.Additive);
        }
    }
}
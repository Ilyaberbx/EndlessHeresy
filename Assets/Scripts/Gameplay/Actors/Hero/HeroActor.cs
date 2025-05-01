using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.UI.Huds.Abilities;
using EndlessHeresy.UI.Huds.Stats;
using EndlessHeresy.UI.Huds.StatusEffects;
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
        private StatusEffectsComponent _statusEffectsStorage;

        [Inject]
        public void Construct(IHudsService hudsService, IGameplayFactoryService gameplayFactoryService)
        {
            _hudsService = hudsService;
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _healthComponent = GetComponent<HealthComponent>();
            _abilitiesStorage = GetComponent<AbilitiesStorageComponent>();
            _statsComponent = GetComponent<StatsComponent>();
            _statusEffectsStorage = GetComponent<StatusEffectsComponent>();

            await Task.WhenAll(ShowAbilitiesHudAsync(), ShowStatsHudAsync(), ShowStatusEffectsHudAsync());
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(10);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                _statusEffectsStorage.Add(StatusEffectType.Deceleration);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                _statusEffectsStorage.Add(StatusEffectType.Acceleration);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                _statusEffectsStorage.Add(StatusEffectType.Burning);
            }
        }

        private Task ShowAbilitiesHudAsync()
        {
            var model = new AbilitiesHudModel(_abilitiesStorage.Abilities);
            return _hudsService.ShowAsync<AbilitiesHudController, AbilitiesHudModel>(model, ShowType.Additive);
        }

        private Task ShowStatusEffectsHudAsync()
        {
            var model = new StatusEffectsHudModel(_statusEffectsStorage);
            return _hudsService.ShowAsync<StatusEffectsHudController, StatusEffectsHudModel>(model, ShowType.Additive);
        }

        private Task ShowStatsHudAsync()
        {
            var model = new StatsHudModel(_statsComponent);
            return _hudsService.ShowAsync<StatsHudController, StatsHudModel>(model, ShowType.Additive);
        }
    }
}
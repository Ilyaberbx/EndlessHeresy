using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Attributes;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.Huds.Abilities;
using EndlessHeresy.UI.Huds.Stats;
using EndlessHeresy.UI.Huds.StatusEffects;
using EndlessHeresy.UI.Modals.Inventory;
using EndlessHeresy.UI.Services.Huds;
using EndlessHeresy.UI.Services.Modals;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IStateMachineContext
    {
        private IHudsService _hudsService;
        private IModalsService _modalsService;

        private HealthComponent _healthComponent;
        private AbilitiesStorageComponent _abilitiesStorage;
        private StatModifiersComponent _statModifiersComponent;
        private StatusEffectsComponent _statusEffectsStorage;
        private AttributesComponent _attributesComponent;

        [Inject]
        public void Construct(IHudsService hudsService, IModalsService modalsService)
        {
            _hudsService = hudsService;
            _modalsService = modalsService;
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _healthComponent = GetComponent<HealthComponent>();
            _abilitiesStorage = GetComponent<AbilitiesStorageComponent>();
            _statModifiersComponent = GetComponent<StatModifiersComponent>();
            _statusEffectsStorage = GetComponent<StatusEffectsComponent>();
            _attributesComponent = GetComponent<AttributesComponent>();

            await Task.WhenAll(ShowAbilitiesHudAsync(), ShowStatsHudAsync(), ShowStatusEffectsHudAsync());
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(new DamageData());
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                _statusEffectsStorage.Add(StatusEffectType.Burning);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                _modalsService.ShowAsync<InventoryModalController, InventoryModalModel>(
                    new InventoryModalModel(_attributesComponent));
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _attributesComponent.Increase(AttributeType.Fervor, 5);
                _attributesComponent.Increase(AttributeType.Insight, 3);
                _attributesComponent.Increase(AttributeType.Vitality, 2);
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
            var model = new StatsHudModel(_statModifiersComponent);
            return _hudsService.ShowAsync<StatsHudController, StatsHudModel>(model, ShowType.Additive);
        }
    }
}
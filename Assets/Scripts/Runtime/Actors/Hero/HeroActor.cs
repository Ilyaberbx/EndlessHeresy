using System.Threading.Tasks;
using EndlessHeresy.Meta.UI.Services.Huds;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.States;
using EndlessHeresy.Runtime.Stats.Modifiers;
using EndlessHeresy.Runtime.StatusEffects;
using UnityEngine;
using VContainer;
using UnityInput = UnityEngine.Input;

namespace EndlessHeresy.Runtime.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IStateMachineContext
    {
        private IHudsService _hudsService;

        private HealthComponent _healthComponent;
        private AbilitiesStorageComponent _abilitiesStorage;
        private StatModifiersComponent _statModifiersComponent;
        private StatusEffectsComponent _statusEffectsStorage;
        private AttributesComponent _attributesComponent;

        [Inject]
        public void Construct(IHudsService hudsService)
        {
            _hudsService = hudsService;
        }

        protected override Task OnInitializeAsync()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _abilitiesStorage = GetComponent<AbilitiesStorageComponent>();
            _statModifiersComponent = GetComponent<StatModifiersComponent>();
            _statusEffectsStorage = GetComponent<StatusEffectsComponent>();
            _attributesComponent = GetComponent<AttributesComponent>();

            return Task.CompletedTask;
            // return Task.WhenAll(ShowAbilitiesHudAsync(), ShowStatsHudAsync(), ShowStatusEffectsHudAsync());
        }

        private void Update()
        {
            if (UnityInput.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(new DamageData());
            }

            if (UnityInput.GetKeyDown(KeyCode.M))
            {
                _statusEffectsStorage.Add(StatusEffectType.Burning);
            }

            if (UnityInput.GetKeyDown(KeyCode.K))
            {
                _attributesComponent.Increase(AttributeType.Fervor, 5);
                _attributesComponent.Increase(AttributeType.Insight, 3);
                _attributesComponent.Increase(AttributeType.Vitality, 2);
            }
        }

        // private Task ShowAbilitiesHudAsync()
        // {
        //     var model = new AbilitiesHudModel(_abilitiesStorage.Abilities);
        //     return _hudsService.ShowAsync<AbilitiesHudController, AbilitiesHudModel>(model, ShowType.Additive);
        // }
        //
        // private Task ShowStatusEffectsHudAsync()
        // {
        //     var model = new StatusEffectsHudModel(_statusEffectsStorage);
        //     return _hudsService.ShowAsync<StatusEffectsHudController, StatusEffectsHudModel>(model, ShowType.Additive);
        // }
        //
        // private Task ShowStatsHudAsync()
        // {
        //     var model = new StatsHudModel(_statModifiersComponent);
        //     return _hudsService.ShowAsync<StatsHudController, StatsHudModel>(model, ShowType.Additive);
        // }
    }
}
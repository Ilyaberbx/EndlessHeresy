using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Huds.StatusEffects;
using EndlessHeresy.Runtime.UI.Modals.Inventory;
using EndlessHeresy.Runtime.UI.Services.Huds;
using EndlessHeresy.Runtime.UI.Services.Modals;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;
using UnityEngine;
using VContainer;
using UnityInput = UnityEngine.Input;

namespace EndlessHeresy.Runtime.Actors.Hero
{
    public sealed class HeroActor : MonoActor
    {
        private IHudsService _hudsService;
        private IModalsService _modalsService;

        private HealthComponent _healthComponent;
        private StatusEffectsComponent _statusEffectsStorage;
        private AttributesComponent _attributesComponent;
        private InventoryComponent _inventoryComponent;

        [Inject]
        public void Construct(IHudsService hudsService, IModalsService modalsService)
        {
            _hudsService = hudsService;
            _modalsService = modalsService;
        }

        protected override Task OnInitializeAsync()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _statusEffectsStorage = GetComponent<StatusEffectsComponent>();
            _attributesComponent = GetComponent<AttributesComponent>();
            _inventoryComponent = GetComponent<InventoryComponent>();

            return Task.WhenAll(ShowStatusEffectsHudAsync());
        }

        private Task ShowStatusEffectsHudAsync()
        {
            var model = new StatusEffectsHudModel(
                new StatusEffectsModel(_statusEffectsStorage.ActiveStatusEffectsReadOnly));

            return _hudsService.ShowAsync<StatusEffectsHudViewModel, StatusEffectsHudModel>(model, ShowType.Additive);
        }
    }
}
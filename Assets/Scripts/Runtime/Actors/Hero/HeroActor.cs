using System.Threading.Tasks;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Huds.StatusEffects;
using EndlessHeresy.Runtime.UI.Services.Huds;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace EndlessHeresy.Runtime.Actors.Hero
{
    public sealed class HeroActor : MonoActor
    {
        [SerializeField] private InputActionReference _cheatsAction;

        private IHudsService _hudsService;
        private StatusEffectsComponent _statusEffectsStorage;

        [Inject]
        public void Construct(IHudsService hudsService)
        {
            _hudsService = hudsService;
        }

        protected override Task OnInitializeAsync()
        {
            _statusEffectsStorage = GetComponent<StatusEffectsComponent>();
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
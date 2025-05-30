using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.UI.World
{
    public sealed class StatusEffectsWorldView : MonoComponent
    {
        [SerializeField] private StatusEffectsView _view;

        private IViewModelFactory _factory;
        private StatusEffectsComponent _statusEffectsComponent;

        [Inject]
        public void Construct(IViewModelFactory factory)
        {
            _factory = factory;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statusEffectsComponent = Owner.GetComponent<StatusEffectsComponent>();
            var model = new StatusEffectsModel(_statusEffectsComponent.ActiveStatusEffectsReadOnly);
            var viewModel = _factory.Create<StatusEffectsViewModel, StatusEffectsModel>(model);
            _view.Initialize(viewModel);
            return Task.CompletedTask;
        }
    }
}
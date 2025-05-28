using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudView : BaseView<StatusEffectsHudViewModel>
    {
        [SerializeField] private StatusEffectsView _statusEffectsView;
        
        protected override void Initialize(StatusEffectsHudViewModel viewModel)
        {
            _statusEffectsView.Initialize(viewModel.ItemsViewModel);
        }
    }
}
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.UI.Huds.Stats
{
    public sealed class StatsHudView : BaseView
    {
        [SerializeField] private ProgressBarView _healthProgressView;
        [SerializeField] private ProgressBarView _manaProgressView;

        public ProgressBarView HealthProgressView => _healthProgressView;
        public ProgressBarView ManaProgressView => _manaProgressView;
    }
}
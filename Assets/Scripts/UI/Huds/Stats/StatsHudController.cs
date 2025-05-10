using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Stats
{
    public sealed class StatsHudController : BaseController<StatsHudModel, StatsHudView>
    {
        private ReadOnlyReactiveProperty<int> _maxHealthStat;
        private ReadOnlyReactiveProperty<int> _maxManaStat;
        private ReadOnlyReactiveProperty<int> _healthStat;
        private ReadOnlyReactiveProperty<int> _manaStat;

        protected override void Show(StatsHudModel model, StatsHudView view)
        {
            base.Show(model, view);

            _maxHealthStat = Model.StatModifiers.GetProcessedStat(StatType.MaxHealth);
            _maxManaStat = Model.StatModifiers.GetProcessedStat(StatType.MaxMana);
            _healthStat = Model.StatModifiers.GetProcessedStat(StatType.CurrentHealth);
            _manaStat = Model.StatModifiers.GetProcessedStat(StatType.CurrentMana);

            _healthStat.SubscribeWithInvoke(OnHealthStatChanged);
            _manaStat.SubscribeWithInvoke(OnManaStatChanged);
        }

        protected override void Hide()
        {
            base.Hide();

            _healthStat.Unsubscribe(OnHealthStatChanged);
            _manaStat.Unsubscribe(OnManaStatChanged);
        }

        private void OnHealthStatChanged(int value)
        {
            var progress = (float)value / _maxHealthStat.Value;
            View.HealthProgressView.SetProgress(progress);
        }

        private void OnManaStatChanged(int value)
        {
            var progress = (float)value / _maxManaStat.Value;
            View.ManaProgressView.SetProgress(progress);
        }
    }
}
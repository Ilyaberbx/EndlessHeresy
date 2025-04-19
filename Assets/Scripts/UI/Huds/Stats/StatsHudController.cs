using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Stats
{
    public sealed class StatsHudController : BaseController<StatsHudModel, StatsHudView>
    {
        private ReactiveProperty<int> _maxHealthStat;
        private ReactiveProperty<int> _maxManaStat;
        private ReactiveProperty<int> _healthStat;
        private ReactiveProperty<int> _manaStat;

        protected override void Show(StatsHudModel model, StatsHudView view)
        {
            base.Show(model, view);

            _maxHealthStat = Model.Stats.GetOrAdd(StatType.MaxHealth);
            _maxManaStat = Model.Stats.GetOrAdd(StatType.MaxMana);
            _healthStat = Model.Stats.GetOrAdd(StatType.Health);
            _manaStat = Model.Stats.GetOrAdd(StatType.Mana);

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
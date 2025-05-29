using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public class PeriodStatModifierEffectComponent : PeriodicStatusEffectComponent, IStatModifierSource
    {
        private readonly PeriodStatModifierData _data;

        public PeriodStatModifierEffectComponent(PeriodStatModifierData data, IGameUpdateService gameUpdateService) :
            base(gameUpdateService)
        {
            _data = data;
        }

        protected override float GetInterval()
        {
            return _data.PerSeconds;
        }

        protected override void OnIntervalTick()
        {
            ProcessStatModification();
        }

        public override void Remove(StatsComponent stats)
        {
            base.Remove(stats);

            if (_data.IsTemporary)
            {
                Stats.RemoveAllModifiersBySource(this);
            }
        }

        private void ProcessStatModification()
        {
            var data = _data.ModifierData;
            var modifier = data.GetStatModifier(this);
            Stats.GetStat(data.StatIdentifier).AddModifier(modifier);
        }
    }
}
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class StatModifierEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IStatModifierSource
    {
        private readonly StatModifierData _data;
        private readonly bool _isTemporary;

        public StatModifierEffectComponent(StatModifierData data, bool isTemporary)
        {
            _data = data;
            _isTemporary = isTemporary;
        }

        public void Apply(StatsComponent stats)
        {
            var modifier = _data.GetStatModifier(this);
            stats.GetStat(_data.StatIdentifier).AddModifier(modifier);
        }

        public void Remove(StatsComponent stats)
        {
            if (_isTemporary)
            {
                stats.RemoveAllModifiersBySource(this);
            }
        }
    }
}
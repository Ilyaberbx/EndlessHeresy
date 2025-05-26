using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class StatModifierStatusEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly StatModifierData _data;
        private StatModifiersComponent _statModifiersComponent;

        public StatModifierStatusEffectComponent(StatModifierData data)
        {
            _data = data;
        }

        public void Initialize(IStatusEffectRoot root)
        {
            _statModifiersComponent = root.Owner.GetComponent<StatModifiersComponent>();
        }

        public void Apply(StatsContainer stats)
        {
            _statModifiersComponent.Process(_data);
        }

        public void Remove(StatsContainer stats)
        {
            _statModifiersComponent.Reverse(_data);
        }
    }
}
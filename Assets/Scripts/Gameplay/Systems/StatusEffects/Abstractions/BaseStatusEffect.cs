using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public abstract class BaseStatusEffect : IStatusEffect
    {
        public abstract void Apply(StatsComponent stats);
        public abstract void Remove(StatsComponent stats);

        public virtual bool TryGet<TStatusEffect>(out TStatusEffect statusEffect)
            where TStatusEffect : class, IStatusEffect
        {
            if (this is TStatusEffect effect)
            {
                statusEffect = effect;
                return true;
            }

            statusEffect = null;
            return false;
        }

        public void SetRoot(IStatusEffect root) => Root = root;
        protected IStatusEffect Root { get; private set; }
    }
}
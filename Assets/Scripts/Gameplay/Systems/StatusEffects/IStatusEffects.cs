using System;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IStatusEffects : IStatusEffectsReadOnly
    {
        void Add(IStatusEffect effects);
    }

    public interface IStatusEffectsReadOnly
    {
        public Action<IStatusEffect> OnStatusEffectAdded { get; set; }
        public Action<IStatusEffect> OnStatusEffectRemoved { get; set; }
        void Remove(IStatusEffect effect);
    }
}
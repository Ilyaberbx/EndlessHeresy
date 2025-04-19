using System;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IStatusEffects : IStatusEffectsReadOnly
    {
        public Action<IStatusEffect> OnStatusEffectAdded { get; set; }
        public Action<IStatusEffect> OnStatusEffectRemoved { get; set; }
        void Add(IStatusEffect effects);
    }

    public interface IStatusEffectsReadOnly
    {
        void Remove(IStatusEffect effect);
    }
}
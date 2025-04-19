using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IUpdatableStatusEffect : IStatusEffect
    {
        void Update(IActor owner);
    }
}
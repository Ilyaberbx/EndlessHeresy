using UniRx;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IStatusEffectsReadOnly
    {
        public IReadOnlyReactiveCollection<IStatusEffectRoot> ActiveStatusEffects { get; }
    }
}
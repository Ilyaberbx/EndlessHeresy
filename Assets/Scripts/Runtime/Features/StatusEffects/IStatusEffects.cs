using Better.Locators.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;
using UniRx;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IStatusEffectsReadOnly
    {
        public IReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>> ActiveStatusEffects { get; }
    }
}
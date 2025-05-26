using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IStatusEffects : IStatusEffectsReadOnly
    {
        void Add(StatusEffectType identifiers);
        void Remove(StatusEffectType identifiers);
    }

    public interface IStatusEffectsReadOnly
    {
        public ReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>> ActiveStatusEffects { get; }
    }
}
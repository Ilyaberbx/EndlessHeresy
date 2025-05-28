using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects.Item
{
    public sealed class StatusEffectItemModel : IModel
    {
        public IStatusEffectRoot StatusEffect { get; }

        public StatusEffectItemModel(IStatusEffectRoot statusEffect)
        {
            StatusEffect = statusEffect;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects
{
    public sealed class StatusEffectsModel : IModel, IEnumerable<IStatusEffectRoot>
    {
        public IReadOnlyReactiveCollection<IStatusEffectRoot> StatusEffects { get; }

        public StatusEffectsModel(IReadOnlyReactiveCollection<IStatusEffectRoot> statusEffects)
        {
            StatusEffects = statusEffects;
        }

        public IEnumerator<IStatusEffectRoot> GetEnumerator()
        {
            return StatusEffects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
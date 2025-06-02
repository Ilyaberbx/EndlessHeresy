using System.Collections;
using System.Collections.Generic;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects
{
    public sealed class StatusEffectsModel : IModel, IEnumerable<StatusEffectRoot>
    {
        public IReadOnlyReactiveCollection<StatusEffectRoot> StatusEffects { get; }

        public StatusEffectsModel(IReadOnlyReactiveCollection<StatusEffectRoot> statusEffects)
        {
            StatusEffects = statusEffects;
        }

        public IEnumerator<StatusEffectRoot> GetEnumerator()
        {
            return StatusEffects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
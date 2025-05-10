using System.Collections.Generic;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudModel : IModel
    {
        public IStatusEffectsReadOnly StatusEffectsReadOnly { get; }

        public StatusEffectsHudModel(IStatusEffectsReadOnly statusEffectsReadOnly)
        {
            StatusEffectsReadOnly = statusEffectsReadOnly;
        }

        public IEnumerable<IStatusEffectRoot> GetActiveStatusEffects() =>
            StatusEffectsReadOnly
                .ActiveStatusEffects
                .Value
                .GetElements();
    }
}
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
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

        public IEnumerable<IStatusEffect> GetActiveStatusEffects() =>
            StatusEffectsReadOnly
                .ActiveStatusEffects
                .Value
                .GetElements();
    }
}
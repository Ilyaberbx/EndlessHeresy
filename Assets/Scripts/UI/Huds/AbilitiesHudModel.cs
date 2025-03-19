using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds
{
    public sealed class AbilitiesHudModel : IModel
    {
        public ReactiveProperty<IEnumerable<Ability>> Abilities { get; } = new();

        public IEnumerable<Ability> GetAbilitiesByState(AbilityState state)
        {
            return Abilities.Value.Where(temp => temp.State.Value == state);
        }

        public void SetAbilities(IEnumerable<Ability> abilities) => Abilities.Value = abilities;
    }
}
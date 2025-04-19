using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Abilities
{
    public sealed class AbilitiesHudModel : IModel
    {
        public ReactiveProperty<IEnumerable<Ability>> Abilities { get; }

        public AbilitiesHudModel(IEnumerable<Ability> abilities)
        {
            Abilities = new ReactiveProperty<IEnumerable<Ability>>(abilities);
        }

        public IEnumerable<Ability> GetAbilitiesByState(AbilityState state)
        {
            return Abilities.Value.Where(temp => temp.State.Value == state);
        }
        
    }
}
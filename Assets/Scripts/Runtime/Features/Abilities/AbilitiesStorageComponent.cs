using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Abilities;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesStorageComponent : PocoComponent
    {
        public IReadOnlyList<Ability> Abilities { get; private set; }

        public AbilitiesStorageComponent(AbilityConfiguration[] abilityConfigurations)
        {
            Abilities = abilityConfigurations
                .Select(configuration => configuration.GetAbility())
                .ToList()
                .AsReadOnly();
        }
    }
}
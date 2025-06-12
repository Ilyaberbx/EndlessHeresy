using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Abilities.New;

namespace EndlessHeresy.Runtime.NewAbilities
{
    public sealed class AbilitiesNewStorageComponent : PocoComponent
    {
        public IReadOnlyList<NewAbility> Abilities { get; private set; }

        public AbilitiesNewStorageComponent(NewAbilityConfiguration[] abilityConfigurations)
        {
            Abilities = abilityConfigurations
                .Select(configuration => configuration.GetAbility())
                .ToList()
                .AsReadOnly();
        }
    }
}
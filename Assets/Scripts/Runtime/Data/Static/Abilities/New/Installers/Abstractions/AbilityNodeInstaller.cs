using System;
using EndlessHeresy.Runtime.NewAbilities.Nodes;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public abstract class AbilityNodeInstaller
    {
        public abstract AbilityNode GetNode();
    }
}
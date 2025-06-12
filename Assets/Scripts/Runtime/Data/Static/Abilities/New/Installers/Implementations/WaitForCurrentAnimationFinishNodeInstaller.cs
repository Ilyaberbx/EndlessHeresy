using System;
using EndlessHeresy.Runtime.NewAbilities.Nodes;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class WaitForCurrentAnimationFinishNodeInstaller : AbilityNodeInstaller
    {
        public override AbilityNode GetNode()
        {
            return new WaitForCurrentAnimationFinishNode();
        }
    }
}
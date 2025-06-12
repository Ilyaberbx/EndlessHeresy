using System;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class WaitForAnimationEventInstaller : AbilityNodeInstaller
    {
        [SerializeField] private string _animationEventName;

        public override AbilityNode GetNode()
        {
            return new WaitForAnimationEventNode(_animationEventName);
        }
    }
}
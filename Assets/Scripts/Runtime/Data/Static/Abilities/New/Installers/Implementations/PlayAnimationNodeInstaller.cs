using System;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class PlayAnimationNodeInstaller : AbilityNodeInstaller
    {
        [SerializeField] private string _animationName;

        public override AbilityNode GetNode()
        {
            return new PlayAnimationNode(_animationName);
        }
    }
}
using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class ParallelNodeInstaller : AbilityNodeInstaller
    {
        [SerializeReference, Select] private AbilityNodeInstaller[] _installers;

        public override AbilityNode GetNode()
        {
            return new ParallelNode(_installers.Select(installer => installer.GetNode()).ToArray());
        }
    }
}
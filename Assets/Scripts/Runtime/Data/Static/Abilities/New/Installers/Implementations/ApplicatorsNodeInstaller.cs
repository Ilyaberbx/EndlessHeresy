using System;
using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class ApplicatorsNodeInstaller : AbilityNodeInstaller
    {
        [SerializeField] private ApplicatorInstaller[] _applicatorInstallers;

        public override AbilityNode GetNode()
        {
            var applicators = _applicatorInstallers.Select(installer => installer.GetApplicator()).ToArray();
            return new ApplicatorsNode(applicators);
        }
    }
}
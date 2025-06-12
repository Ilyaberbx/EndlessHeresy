using System;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public sealed class AbilityMutationApplicatorInstaller : ApplicatorInstaller
    {
        [SerializeField] private AbilityMutationData _mutationData;

        public override IApplicator GetApplicator()
        {
            return new AbilityMutationApplicator(_mutationData);
        }
    }
}
using System;
using EndlessHeresy.Runtime.Applicators;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public sealed class VfxApplicatorInstaller : ApplicatorInstaller
    {
        [SerializeField] private Animator _vfxPrefab;

        public override IApplicator GetApplicator()
        {
            return new VfxApplicator(_vfxPrefab);
        }
    }
}
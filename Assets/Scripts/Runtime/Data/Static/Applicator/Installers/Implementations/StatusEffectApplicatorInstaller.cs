using System;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public sealed class StatusEffectApplicatorInstaller : ApplicatorInstaller
    {
        [SerializeField] private StatusEffectType _identifier;

        public override IApplicator GetApplicator()
        {
            return new StatusEffectApplicator(_identifier);
        }
    }
}
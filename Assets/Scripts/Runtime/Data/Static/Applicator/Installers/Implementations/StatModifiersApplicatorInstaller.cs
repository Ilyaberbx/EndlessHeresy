using System;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public sealed class StatModifiersApplicatorInstaller : ApplicatorInstaller
    {
        [SerializeField] private StatModifierData[] _modifiersData;

        public override IApplicator GetApplicator()
        {
            return new StatModifiersApplicator(_modifiersData);
        }
    }
}
using System;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public sealed class AttributeModifiersApplicatorInstaller : ApplicatorInstaller
    {
        [SerializeField] private AttributeModifierData[] _data;

        public override IApplicator GetApplicator()
        {
            return new AttributeModifiersApplicator(_data);
        }
    }
}
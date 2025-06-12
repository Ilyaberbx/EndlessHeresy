using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct EquipmentSlotApplicatorsData
    {
        [SerializeField] private EquipmentSlotType _identifier;
        [SerializeReference, Select] private ApplicatorInstaller[] _applicatorInstallers;

        public EquipmentSlotType Identifier => _identifier;
        public ApplicatorInstaller[] ApplicatorInstallers => _applicatorInstallers;
    }
}
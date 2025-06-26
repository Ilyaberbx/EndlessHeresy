using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct StatusEffectBehaviourData
    {
        [SerializeReference, Select] private List<StatusEffectComponentInstaller> _installers;

        public List<StatusEffectComponentInstaller> Installers => _installers;
    }
}
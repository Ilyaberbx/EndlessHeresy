using System;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers
{
    [Serializable]
    public sealed class PeriodicEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private PeriodicEffectData[] _data;

        public override void Install(StatusEffectBuilder builder)
        {
            builder.WithComponent<PeriodicEffectComponent>(_data);
        }
    }
}
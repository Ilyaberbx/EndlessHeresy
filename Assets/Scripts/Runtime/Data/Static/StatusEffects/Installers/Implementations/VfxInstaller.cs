using System;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class VfxInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private Animator _vfxPrefab;

        public override void Install(StatusEffectsBuilder builder)
        {
            builder.WithComponent<VfxEffectComponent>(_vfxPrefab);
        }
    }
}
using System;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class TemporaryInstaller : StatusEffectComponentInstaller
    {
        [SerializeField, Min(0)] private float _duration;

        public override void Install(StatusEffectsBuilder builder)
        {
            builder.WithComponent<TemporaryEffectComponent>(_duration);
        }
    }
}
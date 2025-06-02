using System;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class PeriodDamageInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private PeriodDamageData _data;

        public override void Install(StatusEffectsBuilder builder)
        {
            var timer = new Timer.Timer(_data.PerSeconds);
            builder.WithComponent<PeriodDamageEffectComponent>(_data.DamageData, timer);
            builder.WithComponent<TimerEffectComponent>(timer);
        }
    }
}
using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class PeriodApplicatorInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private float _perSeconds;
        [SerializeReference, Select] private ApplicatorInstaller _installer;

        public override void Install(StatusEffectsBuilder builder)
        {
            var timer = new Timer.Timer(_perSeconds);
            builder.WithComponent(new PeriodApplicatorEffectComponent(_installer.GetApplicator(), timer));
            builder.WithComponent<TimerEffectComponent>(timer);
        }
    }
}
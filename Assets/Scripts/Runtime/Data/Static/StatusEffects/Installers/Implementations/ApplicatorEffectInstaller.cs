using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class ApplicatorEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeReference, Select] private ApplicatorInstaller _installer;

        public override void Install(StatusEffectsBuilder builder)
        {
            builder.WithComponent(new ApplicatorEffectComponent(_installer.GetApplicator()));
        }
    }
}
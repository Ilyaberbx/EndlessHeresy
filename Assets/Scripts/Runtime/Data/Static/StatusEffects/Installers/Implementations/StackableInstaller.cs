using System;
using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class StackableInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private List<StatusEffectConfiguration> _installers;

        public override void Install(StatusEffectsBuilder builder)
        {
            var builders = new List<StatusEffectsBuilder>();

            foreach (var installer in _installers)
            {
                var internalBuilder = new StatusEffectsBuilder(builder.Resolver);
                installer.ConfigureBuilder(internalBuilder);
                builders.Add(internalBuilder);
            }

            builder.WithComponent<StackableEffectComponent>(builders);
        }
    }
}
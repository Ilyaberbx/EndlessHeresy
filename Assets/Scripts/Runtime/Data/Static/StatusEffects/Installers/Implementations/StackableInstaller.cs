using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
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
                var internalBuilder = new StatusEffectsBuilder();
                installer.ConfigureBuilder(internalBuilder);
                builders.Add(internalBuilder);
            }

            builder.WithComponent<StackableStatusEffectComponent>(builders);
        }
    }
}
using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class CommandEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeReference, Select] private CommandInstaller _commandInstaller;

        public override void Install(StatusEffectsBuilder builder)
        {
            builder.WithComponent(new CommandEffectComponent(_commandInstaller));
        }
    }
}
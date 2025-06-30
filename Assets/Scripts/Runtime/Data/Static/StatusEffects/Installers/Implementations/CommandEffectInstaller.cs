using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers
{
    [Serializable]
    public sealed class CommandEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeReference, Select] private ICommandInstaller _commandInstaller;

        public override void Install(StatusEffectBuilder builder)
        {
            builder.WithComponent(new CommandEffectComponent(_commandInstaller.GetCommand()));
        }
    }
}
using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers
{
    [Serializable]
    public sealed class UndoableEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeReference, Select] private UndoableCommandInstaller[] _undoableCommandInstallers;

        public override void Install(StatusEffectBuilder builder)
        {
            var commands = _undoableCommandInstallers.Select(temp => temp.GetUndoableCommand()).ToArray();
            builder.WithComponent(new UndoableEffectComponent(commands));
        }
    }
}
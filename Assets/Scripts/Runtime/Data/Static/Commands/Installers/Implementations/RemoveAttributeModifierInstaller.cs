using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class RemoveAttributeModifierInstaller : UndoableCommandInstaller
    {
        [SerializeField] private AttributeModifierData _data;

        public override IUndoableCommand GetUndoableCommand()
        {
            return new RemoveAttributeModifierCommand(_data);
        }

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return GetUndoableCommand();
        }
    }
}
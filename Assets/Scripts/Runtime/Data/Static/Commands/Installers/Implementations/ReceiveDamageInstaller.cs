using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Damage;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ReceiveDamageInstaller : UndoableCommandInstaller
    {
        [SerializeField] private DamageData _data;

        public override IUndoableCommand GetUndoableCommand()
        {
            return new ReceiveDamageCommand(_data);
        }

        public override ICommand GetCommand()
        {
            return GetUndoableCommand();
        }
    }
}
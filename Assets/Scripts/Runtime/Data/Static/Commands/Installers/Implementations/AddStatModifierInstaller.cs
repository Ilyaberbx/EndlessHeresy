using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Stats;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Data.Static.Stats;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class AddStatModifierInstaller : UndoableCommandInstaller
    {
        [SerializeField] private StatModifierData _data;
        [SerializeField] private StatModifierSourceAsset _sourceAsset;

        public override IUndoableCommand GetUndoableCommand()
        {
            return new AddStatModifierCommand(_data, _sourceAsset);
        }

        public override ICommand GetCommand()
        {
            return GetUndoableCommand();
        }
    }
}
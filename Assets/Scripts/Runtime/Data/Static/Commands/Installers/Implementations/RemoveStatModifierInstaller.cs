using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Stats;
using EndlessHeresy.Runtime.Data.Static.Stats;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class RemoveStatModifierInstaller : ICommandInstaller
    {
        [SerializeField] private StatModifierSourceAsset _sourceAsset;

        public ICommand GetCommand()
        {
            return new RemoveStatModifierCommand(_sourceAsset);
        }
    }
}
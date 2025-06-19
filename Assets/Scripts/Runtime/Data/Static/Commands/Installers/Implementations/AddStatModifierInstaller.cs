using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Stats;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class AddStatModifierInstaller : ICommandInstaller
    {
        [SerializeField] private StatModifierData _data;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new AddStatModifier(_data);
        }
    }
}
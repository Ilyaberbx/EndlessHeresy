using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Stats;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class AddStatModifierInstaller : CommandInstaller
    {
        [SerializeField] private StatModifierData _data;

        public override ICommand GetCommand()
        {
            return new AddStatModifier(_data);
        }
    }
}
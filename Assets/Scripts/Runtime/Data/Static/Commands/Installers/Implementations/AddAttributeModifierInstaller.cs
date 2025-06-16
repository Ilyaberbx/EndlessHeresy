using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class AddAttributeModifierInstaller : CommandInstaller
    {
        [SerializeField] private AttributeModifierData _data;

        public override ICommand GetCommand()
        {
            return new AddAttributeModifier(_data);
        }
    }
}
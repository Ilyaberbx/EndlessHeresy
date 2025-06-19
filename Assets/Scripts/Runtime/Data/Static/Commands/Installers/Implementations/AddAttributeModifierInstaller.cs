using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class AddAttributeModifierInstaller : ICommandInstaller
    {
        [SerializeField] private AttributeModifierData _data;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new AddAttributeModifier(_data);
        }
    }
}
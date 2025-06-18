using System;
using Better.Attributes.Runtime.Select;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ForkInstaller : CommandInstaller
    {
        [SerializeReference, Select] private Condition _condition;
        [SerializeReference, Select] private CommandInstaller _true;
        [SerializeReference, Select] private CommandInstaller _false;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new Fork(_condition, _true.GetCommand(resolver), _false.GetCommand(resolver));
        }
    }
}
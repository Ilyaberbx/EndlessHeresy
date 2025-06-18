using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Core;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealMeleeAttackInstaller : CommandInstaller
    {
        [SerializeField] private MeleeAttackData _data;
        [SerializeReference, Select] private CommandInstaller _targetCommandInstaller;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealMeleeAttack(resolver, _data, _targetCommandInstaller);
        }
    }
}
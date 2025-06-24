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
    public sealed class DealMeleeAttackInstaller : ICommandInstaller
    {
        [SerializeField] private MeleeAttackConfiguration _data;
        [SerializeReference, Select] private ICommandInstaller _targetCommandInstaller;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealMeleeAttack(resolver, _data, _targetCommandInstaller);
        }
    }
}
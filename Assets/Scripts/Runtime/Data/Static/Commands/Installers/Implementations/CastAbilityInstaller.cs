using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class CastAbilityInstaller : CommandInstaller
    {
        [SerializeField] private AbilityType _identifier;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new CastAbility(_identifier);
        }
    }
}
using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class CastAbilityInstaller : CommandInstaller
    {
        [SerializeField] private AbilityType _identifier;

        public override ICommand GetCommand()
        {
            return new CastAbility(_identifier);
        }
    }
}
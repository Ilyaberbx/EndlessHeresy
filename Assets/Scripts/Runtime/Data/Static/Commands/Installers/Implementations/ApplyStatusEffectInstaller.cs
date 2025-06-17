using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.StatusEffects;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ApplyStatusEffectInstaller : CommandInstaller
    {
        [SerializeField] private StatusEffectType _identifier;

        public override ICommand GetCommand()
        {
            return new ApplyStatusEffect(_identifier);
        }
    }
}
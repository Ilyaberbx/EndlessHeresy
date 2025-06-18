using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.StatusEffects;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ApplyStatusEffectInstaller : CommandInstaller
    {
        [SerializeField] private StatusEffectType _identifier;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new ApplyStatusEffect(_identifier);
        }
    }
}
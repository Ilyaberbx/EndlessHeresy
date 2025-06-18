using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Core;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealTakingDamageInstaller : CommandInstaller
    {
        [SerializeField] private DamageData _data;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealTakingDamage(_data);
        }
    }
}
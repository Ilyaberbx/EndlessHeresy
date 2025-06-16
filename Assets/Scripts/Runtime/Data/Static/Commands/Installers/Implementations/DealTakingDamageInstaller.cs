using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Attack;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealTakingDamageInstaller : CommandInstaller
    {
        [SerializeField] private DamageData _data;

        public override ICommand GetCommand()
        {
            return new DealTakingDamage(_data);
        }
    }
}
using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Attack;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealHealingInstaller : CommandInstaller
    {
        [SerializeField] private float _value;

        public override ICommand GetCommand()
        {
            return new DealHealing(_value);
        }
    }
}
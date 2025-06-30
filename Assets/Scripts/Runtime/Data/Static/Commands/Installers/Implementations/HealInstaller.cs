using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Healing;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class HealInstaller : ICommandInstaller
    {
        [SerializeField] private float _value;
        
        public ICommand GetCommand()
        {
            return new HealCommand(_value);
        }
    }
}
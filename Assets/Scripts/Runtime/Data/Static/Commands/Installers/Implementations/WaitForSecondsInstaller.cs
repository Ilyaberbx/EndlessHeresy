using System;
using EndlessHeresy.Runtime.Commands;
using UnityEngine;
using VContainer;
using WaitForSeconds = EndlessHeresy.Runtime.Commands.Supporting.WaitForSeconds;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class WaitForSecondsInstaller : ICommandInstaller
    {
        [SerializeField, Min(0f)] private float _seconds;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new WaitForSeconds(_seconds);
        }
    }
}
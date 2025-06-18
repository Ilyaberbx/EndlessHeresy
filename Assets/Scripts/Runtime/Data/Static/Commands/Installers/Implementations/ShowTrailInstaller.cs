using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Vfx;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ShowTrailInstaller : CommandInstaller
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _lifeTime;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new ShowTrail(_color, _lifeTime);
        }
    }
}
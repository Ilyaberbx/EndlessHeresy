using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Core;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealExplosionForceInstaller : CommandInstaller
    {
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private float _multiplier;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealExplosionForceImpulse(_offSet, _overlapData, _multiplier);
        }
    }
}
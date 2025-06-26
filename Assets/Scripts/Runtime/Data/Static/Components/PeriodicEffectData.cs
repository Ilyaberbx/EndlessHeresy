using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [System.Serializable]
    public struct PeriodicEffectData
    {
        [SerializeReference, Select] private ICommandInstaller _commandInstaller;
        [SerializeField] private float _cooldown;

        public ICommandInstaller CommandInstaller => _commandInstaller;
        public float Cooldown => _cooldown;
    }
}
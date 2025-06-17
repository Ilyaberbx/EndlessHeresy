using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components.Controls
{
    [Serializable]
    public struct AbilityControlsData
    {
        [SerializeField] private AbilityType _abilityIdentifier;
        [SerializeField] private KeyCode _keyCode;

        public AbilityType AbilityIdentifier => _abilityIdentifier;
        public KeyCode KeyCode => _keyCode;
    }
}
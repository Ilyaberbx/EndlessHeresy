using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AbilityInputData
    {
        [SerializeField] private InputActionReference _actionReference;
        [SerializeField] private InputActionPhase _actionPhase;
        [SerializeField] private AbilityType _abilityIdentifier;

        public InputAction Action => _actionReference.action;
        public AbilityType AbilityIdentifier => _abilityIdentifier;
        public InputActionPhase ActionPhase => _actionPhase;
    }
}
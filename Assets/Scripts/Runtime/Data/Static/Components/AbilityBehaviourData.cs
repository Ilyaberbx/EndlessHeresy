using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using Unity.Behavior;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AbilityBehaviourData
    {
        [SerializeField] private AbilityType _identifier;
        [SerializeField] private BehaviorGraph _graph;

        public AbilityType Identifier => _identifier;
        public BehaviorGraph Graph => _graph;
    }
}
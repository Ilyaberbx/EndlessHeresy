using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AbilityMutationData
    {
        [SerializeReference, Select] private SerializedType _serializedType;
        [SerializeField] private bool _insertAfter;
        [SerializeField] private int _occurenceIndex;
        [SerializeField] private AbilityType _abilityIdentifier;
        [SerializeReference, Select] private AbilityNodeInstaller _nodeInstaller;

        public SerializedType TargetSerializedType => _serializedType;
        public bool InsertAfter => _insertAfter;
        public int OccurenceIndex => _occurenceIndex;
        public AbilityType AbilityIdentifier => _abilityIdentifier;
        public readonly AbilityNode GetNode() => _nodeInstaller.GetNode();
    }
}
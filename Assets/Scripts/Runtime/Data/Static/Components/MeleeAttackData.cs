using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct MeleeAttackData
    {
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private DamageData _damageData;
        
        public CapsuleOverlapData OverlapData => _overlapData;
        public Vector2 OffSet => _offSet;
        public DamageData DamageData => _damageData;
    }
}
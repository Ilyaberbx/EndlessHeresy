using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct MeleeAttackData
    {
        [SerializeField] private float _forceMultiplier;
        [SerializeField] private float _dragForceMultiplier;
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private DamageData _damageData;

        public float ForceMultiplier => _forceMultiplier;
        public float DragForceMultiplier => _dragForceMultiplier;
        public CapsuleOverlapData OverlapData => _overlapData;
        public Vector2 OffSet => _offSet;
        public DamageData DamageData => _damageData;
    }
}
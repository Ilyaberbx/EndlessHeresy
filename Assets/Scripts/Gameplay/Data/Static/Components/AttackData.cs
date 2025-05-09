using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct AttackData
    {
        [SerializeField] private float _force;
        [SerializeField] private float _dragForce;
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private DamageData _damageData;
        [SerializeField] private StatPercentageData[] _statsDamageData;

        public float Force => _force;
        public float DragForce => _dragForce;
        public CapsuleOverlapData OverlapData => _overlapData;
        public Vector2 OffSet => _offSet;
        public DamageData DamageData => _damageData;
    }
}
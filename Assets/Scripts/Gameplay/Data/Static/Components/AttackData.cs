using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct AttackData
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _force;
        [SerializeField] private float _dragForce;
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private Vector2 _offSet;
        

        public int Damage => _damage;
        public float Force => _force;
        public float DragForce => _dragForce;
        public CapsuleOverlapData OverlapData => _overlapData;

        public Vector2 OffSet => _offSet;
    }
}
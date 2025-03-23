using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public sealed class AttackData
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _force;
        [SerializeField] private float _radius;
        [SerializeField] private float _dragForce;

        public int Damage => _damage;
        public float Force => _force;
        public float Radius => _radius;
        public float DragForce => _dragForce;
    }
}
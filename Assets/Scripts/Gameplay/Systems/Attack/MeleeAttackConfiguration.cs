using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Attack
{
    [Serializable]
    public sealed class MeleeAttackConfiguration
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _radius;

        public int Damage => _damage;

        public float Radius => _radius;
    }
}
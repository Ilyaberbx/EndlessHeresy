using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct PeriodDamageData
    {
        [SerializeField] private float _perSeconds;
        [SerializeField] private int _damage;

        public float PerSeconds => _perSeconds;
        public int Damage => _damage;
    }
}
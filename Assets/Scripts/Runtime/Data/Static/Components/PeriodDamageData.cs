using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct PeriodDamageData
    {
        [SerializeField] private float _perSeconds;
        [SerializeField] private DamageData _damageData;

        public float PerSeconds => _perSeconds;
        public DamageData DamageData => _damageData;
    }
}
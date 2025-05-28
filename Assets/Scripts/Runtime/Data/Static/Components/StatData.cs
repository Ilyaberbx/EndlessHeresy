using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct StatData
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private StatType _identifier;

        public Stat GetStat()
        {
            return new Stat(_baseValue, _identifier);
        }
    }
}
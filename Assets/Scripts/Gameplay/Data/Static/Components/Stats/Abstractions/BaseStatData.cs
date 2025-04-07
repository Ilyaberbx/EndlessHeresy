using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using EndlessHeresy.Gameplay.Stats;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components.Stats
{
    [Serializable]
    public abstract class BaseStatData
    {
        [SerializeField, Select(typeof(BaseStat))] private SerializedType _identifier;
        public Type Identifier => _identifier.Type;
        public abstract BaseStat GetStat();
    }

    [Serializable]
    public abstract class BaseStatData<TValue> : BaseStatData
    {
        [SerializeField] private TValue _defaultValue;
        public TValue DefaultValue => _defaultValue;
    }
}
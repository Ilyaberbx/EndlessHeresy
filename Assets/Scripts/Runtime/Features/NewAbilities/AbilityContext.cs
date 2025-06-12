using System;
using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Operational.Abilities;

namespace EndlessHeresy.Runtime.NewAbilities
{
    public sealed class AbilityContext
    {
        public IActor Caster { get; }
        public NewAbility Entity { get; }

        private readonly Dictionary<Type, AbilityNodeData> _typedData;

        public AbilityContext(IActor caster, NewAbility entity)
        {
            Caster = caster;
            Entity = entity;
            _typedData = new Dictionary<Type, AbilityNodeData>();
        }

        public void Set<T>(T data) where T : AbilityNodeData
        {
            _typedData[typeof(T)] = data;
        }

        public bool TryGet<T>(out T data) where T : AbilityNodeData
        {
            if (_typedData.TryGetValue(typeof(T), out var val) && val is T casted)
            {
                data = casted;
                return true;
            }

            data = null;
            return false;
        }

        public T Get<T>() where T : AbilityNodeData, new()
        {
            if (TryGet<T>(out var data)) return data;

            var newData = new T();
            _typedData[typeof(T)] = newData;
            return newData;
        }

        public bool Has<T>() where T : AbilityNodeData => _typedData.ContainsKey(typeof(T));
    }
}
using System;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers
{
    [Serializable]
    public sealed class StackableEffectInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private int _maxStacks;
        [SerializeField] private StatusEffectBehaviourData[] _behavioursData;

        public override void Install(StatusEffectBuilder builder)
        {
            builder.WithComponent<StackableEffectComponent>(_maxStacks, _behavioursData);
        }
    }
}
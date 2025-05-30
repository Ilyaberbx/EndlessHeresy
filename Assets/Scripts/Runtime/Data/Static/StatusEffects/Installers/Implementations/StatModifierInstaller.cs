using System;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Implementations
{
    [Serializable]
    public sealed class StatModifierInstaller : StatusEffectComponentInstaller
    {
        [SerializeField] private bool _isTemporary;
        [SerializeField] private StatModifierData _data;

        public override void Install(StatusEffectsBuilder builder)
        {
            builder.WithComponent<StatModifierEffectComponent>(_data, _isTemporary);
        }
    }
}
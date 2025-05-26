using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/PermanentStatModifierStatusEffect",
        fileName = "PermanentModiferStatusEffectConfiguration", order = 0)]
    public sealed class PermanentModiferStatusEffectConfiguration : StatusEffectConfiguration
    {
        [SerializeField] private StatModifierData _modifierData;

        public override void ConfigureBuilder(StatusEffectsBuilder builder)
        {
            base.ConfigureBuilder(builder);

            builder.WithComponent(new StatModifierStatusEffectComponent(_modifierData));
        }
    }
}
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Builder;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/BurningStatusEffect",
        fileName = "BurningStatusEffectConfiguration", order = 0)]
    public sealed class BurningStatusEffectConfiguration : StatusEffectConfiguration
    {
        [SerializeField] private PeriodDamageData _periodDamageData;
        [SerializeField] private Animator _vfxPrefab;
        [SerializeField] private float _duration;

        public override void ConfigureBuilder(StatusEffectsBuilder builder)
        {
            base.ConfigureBuilder(builder);

            builder.WithComponent(new VfxStatusEffectComponent(_vfxPrefab));
            builder.WithComponent(new TemporaryStatusEffectComponent(_duration));
            builder.WithComponent(new StackableStatusEffectComponent(GetProgression));
        }

        private IStatusEffectComponent GetProgression(int stack)
        {
            return new PeriodDamageStatusEffectComponent(_periodDamageData);
        }
    }
}
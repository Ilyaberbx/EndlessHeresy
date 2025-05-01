using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/Burning", fileName = "BurningStatusEffectConfiguration",
        order = 0)]
    public sealed class BurningStatusEffectConfiguration : StatusEffectConfiguration
    {
        [SerializeField] private PeriodDamageData _periodDamageData;
        [SerializeField] private float _duration;
        [SerializeField] private Animator _vfxPrefab;


        public override IStatusEffect GetStatusEffect()
        {
            var vfx = new VfxStatusEffect(_vfxPrefab);
            var stackable = new StackableStatusEffect(GetProgression);
            var temporary = new TemporaryStatusEffect(stackable, _duration);
            var stackDurationSync = new StackDurationSynchronizer(stackable, temporary);
            var identifiedStatusEffect = new IdentifiedStatusEffect(Identifier, stackDurationSync);
            var root = new ComplexStatusEffect(identifiedStatusEffect, vfx);
            vfx.SetRoot(root);
            stackable.SetRoot(root);
            temporary.SetRoot(root);
            stackDurationSync.SetRoot(root);
            return root;
        }

        private IStatusEffect GetProgression(int stack)
        {
            return new PeriodDamageStatusEffect(_periodDamageData);
        }
    }
}
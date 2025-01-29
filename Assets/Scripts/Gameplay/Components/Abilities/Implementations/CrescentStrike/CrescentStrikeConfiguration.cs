using EndlessHeresy.Gameplay.Actors.CrescentKnife;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    [CreateAssetMenu(menuName = "Configs/Ability/CrescentStrike", fileName = "CrescentStrikeAbility", order = 0)]
    public sealed class CrescentStrikeConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _duration;
        [SerializeField] private float _knifeOffset;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private CrescentKnifeActor _crescentKnifePrefab;
        public int Damage => _damage;
        public float Duration => _duration;
        public AnimationCurve Curve => _curve;
        public CrescentKnifeActor CrescentKnifePrefab => _crescentKnifePrefab;
        public float KnifeOffset => _knifeOffset;
        public override AbilityBuilder GetBuilder() => new CrescentStrikeBuilder(this);
    }
}
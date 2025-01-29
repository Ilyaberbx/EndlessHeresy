using EndlessHeresy.Gameplay.Actors.CrescentKnife;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    [CreateAssetMenu(menuName = "Configs/Ability/CrescentStrike", fileName = "CrescentStrikeAbility", order = 0)]
    public sealed class CrescentStrikeConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _knifeOffset;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private CrescentKnifeActor _crescentKnifePrefab;
        public int Damage => _damage;
        public float Speed => _speed;
        public AnimationCurve Curve => _curve;
        public CrescentKnifeActor CrescentKnifePrefab => _crescentKnifePrefab;
        public float KnifeOffset => _knifeOffset;
        public override AbilityBuilder GetBuilder() => new CrescentStrikeBuilder(this);
    }
}
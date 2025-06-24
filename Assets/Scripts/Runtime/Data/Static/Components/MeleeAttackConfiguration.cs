using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/MeleeAttack", fileName = "MeleeAttackConfiguration", order = 0)]
    public sealed class MeleeAttackConfiguration : ScriptableObject
    {
        [SerializeField] private CapsuleOverlapData _overlapData;
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private DamageData _damageData;

        public CapsuleOverlapData OverlapData => _overlapData;
        public Vector2 OffSet => _offSet;
        public DamageData DamageData => _damageData;
    }
}
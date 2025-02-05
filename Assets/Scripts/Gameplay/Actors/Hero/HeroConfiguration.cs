using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Attack;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private HeroActor _prefab;
        [SerializeField] private AbilityConfiguration[] _abilities;
        [SerializeField] private MeleeAttackConfiguration _meleeAttack;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _health;
        public HeroActor Prefab => _prefab;
        public AbilityConfiguration[] Abilities => _abilities;
        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public MeleeAttackConfiguration MeleeAttack => _meleeAttack;
    }
}
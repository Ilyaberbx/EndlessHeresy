using EndlessHeresy.Gameplay.Abilities;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private HeroActor _prefab;
        [SerializeField] private AbilityConfiguration[] _abilities;
        [SerializeField] private float _movementSpeed;
        public HeroActor Prefab => _prefab;
        public AbilityConfiguration[] Abilities => _abilities;
        public float MovementSpeed => _movementSpeed;
    }
}
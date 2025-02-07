using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityConfiguration : ScriptableObject
    {
        [SerializeField] private KeyCode _hotKey;
        [SerializeField] private float _cooldown;

        public KeyCode HotKey => _hotKey;
        public float Cooldown => _cooldown;
        public abstract AbilityBuilder GetBuilder();
    }
}
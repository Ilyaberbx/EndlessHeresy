using EndlessHeresy.Gameplay.Abilities.Enums;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityConfiguration : ScriptableObject
    {
        [SerializeField] private AbilityType _type;

        public AbilityType Type => _type;

        public abstract AbilityBuilder GetBuilder(IObjectResolver container);
    }
}
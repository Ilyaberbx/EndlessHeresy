using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityConfiguration : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private AbilityType _type;

        public AbilityType Type => _type;
        public Sprite Icon => _icon;
        public abstract AbilityFactory GetFactory(IObjectResolver container);
    }
}
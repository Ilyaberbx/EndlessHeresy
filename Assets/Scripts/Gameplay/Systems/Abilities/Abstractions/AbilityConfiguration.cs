using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityConfiguration : ScriptableObject
    {
        public abstract AbilityBuilder GetBuilder(IObjectResolver container);
    }
}
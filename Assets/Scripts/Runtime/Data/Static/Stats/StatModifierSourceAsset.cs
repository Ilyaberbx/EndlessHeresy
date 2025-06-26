using EndlessHeresy.Runtime.Stats.Modifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Stats
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stats/Modifiers", fileName = "StatModifierSource", order = 0)]
    public sealed class StatModifierSourceAsset : ScriptableObject, IStatModifierSource
    {
    }
}
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Damage/Colors", fileName = "DamageColorsConfiguration", order = 0)]
    public sealed class DamageColorsConfiguration : ScriptableObject
    {
        [SerializeField] private DamageColorData[] _data;

        public DamageColorData[] Data => _data;
    }
}
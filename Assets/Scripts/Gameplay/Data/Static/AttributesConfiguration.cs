using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Attributes", fileName = "AttributesConfiguration", order = 0)]
    public sealed class AttributesConfiguration : ScriptableObject
    {
        [SerializeField] private AttributeConfigurationData[] _data;

        public AttributeConfigurationData[] Data => _data;
    }
}
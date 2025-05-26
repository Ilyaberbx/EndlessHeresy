using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Attributes", fileName = "AttributesConfiguration", order = 0)]
    public sealed class AttributesConfiguration : ScriptableObject
    {
        [SerializeField] private AttributeItemData[] _data;

        public AttributeItemData[] Data => _data;
    }
}
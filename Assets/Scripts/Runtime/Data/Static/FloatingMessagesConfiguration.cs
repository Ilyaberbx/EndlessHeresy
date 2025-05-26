using EndlessHeresy.Meta.UI.ViewComponents;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/FloatingMessages", fileName = "FloatingMessagesConfiguration",
        order = 0)]
    public sealed class FloatingMessagesConfiguration : ScriptableObject
    {
        [SerializeField] private FloatingMessageView _prefab;
        [SerializeField] private PoolData _poolData;

        public FloatingMessageView Prefab => _prefab;
        public PoolData PoolData => _poolData;
    }
}
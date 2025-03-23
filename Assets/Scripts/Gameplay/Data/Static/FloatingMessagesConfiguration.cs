using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
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
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Items
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Items/Pickable Item", fileName = "PickableItemConfiguration", order = 0)]
    public sealed class PickableItemConfiguration : ItemConfiguration
    {
        [SerializeField] private ItemPickUpActor _pickUpPrefab;

        public ItemPickUpActor PickUpPrefab => _pickUpPrefab;
    }
}
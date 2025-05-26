using EndlessHeresy.Runtime.Actors;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Items
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Items/Pickable Item", fileName = "PickableItemConfiguration", order = 0)]
    public sealed class PickableItemConfiguration : ItemConfiguration
    {
        [SerializeField] private ItemPickUpActor _pickUpPrefab;

        public ItemPickUpActor PickUpPrefab => _pickUpPrefab;
    }
}
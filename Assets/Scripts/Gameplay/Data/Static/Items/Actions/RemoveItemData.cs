using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Inventory.Actions;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Items.Actions
{
    [Serializable]
    public sealed class RemoveItemData : ItemActionData
    {
        [SerializeField] private ItemType _type;

        public override ItemAction GetAction() => new RemoveItemAction(_type);
    }
}
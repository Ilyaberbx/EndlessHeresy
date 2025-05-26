using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Actions;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Items.Actions
{
    [Serializable]
    public sealed class RemoveItemData : ItemActionData
    {
        [SerializeField] private ItemType _type;

        public override ItemAction GetAction() => new RemoveItemAction(_type);
    }
}
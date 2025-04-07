using System;
using EndlessHeresy.Gameplay.Inventory.Actions;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Items.Actions
{
    [Serializable]
    public sealed class HealItemActionData : ItemActionData
    {
        [SerializeField, Range(0, 10000)] private int _healAmount;

        public override ItemAction GetAction()
        {
            var action = new HealItemAction();
            action.SetHealAmount(_healAmount);
            return action;
        }
    }
}
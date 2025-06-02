using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Inventory.Installers;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Inventory/Item", fileName = "InventoryItemConfiguration", order = 0)]
    public sealed class InventoryItemConfiguration : ScriptableObject
    {
        [SerializeField] private ItemType _identifier;
        [SerializeReference, Select] private ItemComponentInstaller[] _installers;

        public ItemType Identifier => _identifier;

        public ItemRoot GetInstance()
        {
            return new ItemRoot(_identifier, _installers.Select(temp => temp.GetComponent()));
        }
    }
}
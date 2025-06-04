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
        [SerializeField] private Sprite _icon;
        [SerializeField, TextArea] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeReference, Select] private ItemComponentInstaller[] _installers;

        public ItemType Identifier => _identifier;
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;

        public ItemRoot GetInstance()
        {
            var components = _installers.Select(temp => temp.GetComponent());
            return new ItemRoot(_identifier, components.ToArray());
        }
    }
}
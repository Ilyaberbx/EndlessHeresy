using Better.Attributes.Runtime.Select;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Items.Actions;
using EndlessHeresy.Gameplay.Inventory.Actions;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Items
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Items/Simple Item", fileName = "ItemConfiguration", order = 0)]
    public class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private bool _stackable;

        [SerializeReference, Select(typeof(ItemActionData))]
        private ItemActionData[] _storeActionsConfiguration;

        [SerializeReference, Select(typeof(ItemActionData))]
        private ItemActionData[] _contextActionsConfiguration;

        [SerializeReference, Select(typeof(ItemActionData))]
        private ItemActionData[] _removeActionsConfiguration;

        public ItemType Type => _type;
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public bool Stackable => _stackable;

        public ItemAction GetOnStoreAction()
        {
            var complexAction = new ComplexItemAction();

            foreach (var storeActionConfiguration in _storeActionsConfiguration)
            {
                var action = storeActionConfiguration.GetAction();
                complexAction.Add(action);
            }

            return complexAction;
        }

        public ItemAction GetOnRemoveAction()
        {
            var complexAction = new ComplexItemAction();

            foreach (var removeActionConfiguration in _removeActionsConfiguration)
            {
                var action = removeActionConfiguration.GetAction();
                complexAction.Add(action);
            }

            return complexAction;
        }
    }
}
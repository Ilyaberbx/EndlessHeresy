using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Items.Factory;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    public abstract class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private bool _stackable;
        
        public ItemType Type => _type;
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public bool Stackable => _stackable;
    }
}
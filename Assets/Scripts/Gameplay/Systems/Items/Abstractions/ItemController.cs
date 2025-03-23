using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Items.Abstractions
{
    public abstract class ItemController
    {
        public ItemType Type { get; private set; }

        public void SetType(ItemType type)
        {
            Type = type;
        }
    }
}
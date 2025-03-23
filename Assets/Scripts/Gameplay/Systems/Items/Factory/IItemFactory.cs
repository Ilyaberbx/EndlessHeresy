using EndlessHeresy.Gameplay.Items.Abstractions;

namespace EndlessHeresy.Gameplay.Items.Factory
{
    public interface IItemFactory
    {
        public ItemController Create();
    }
}
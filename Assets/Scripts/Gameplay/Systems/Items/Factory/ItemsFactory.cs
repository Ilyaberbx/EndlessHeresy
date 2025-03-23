using EndlessHeresy.Gameplay.Items.Abstractions;
using VContainer;

namespace EndlessHeresy.Gameplay.Items.Factory
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly IObjectResolver _resolver;

        public ItemFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public ItemController Create()
        {
            throw new System.NotImplementedException();
        }
    }
}
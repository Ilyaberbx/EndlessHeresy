using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class StackableItemComponent : IItemComponent
    {
        private readonly ReactiveProperty<int> _stackCount;
        public bool MaxStackSize { get; }
        public IReadOnlyReactiveProperty<int> StackCount => _stackCount;

        public StackableItemComponent(bool maxStackSize)
        {
            MaxStackSize = maxStackSize;
            _stackCount = new ReactiveProperty<int>(1);
        }

        public void AddStack()
        {
            _stackCount.Value++;
        }
    }
}
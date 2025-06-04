using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class StackableItemComponent : IItemComponent
    {
        private const int InitialValue = 1;
        private readonly int _maxStackSize;
        private readonly ReactiveProperty<int> _stackCountProperty;
        public IReadOnlyReactiveProperty<int> StackCountProperty => _stackCountProperty;
        public bool HasFreeSpace => _stackCountProperty.Value < _maxStackSize;

        public StackableItemComponent(int maxStackSize)
        {
            _maxStackSize = maxStackSize;
            _stackCountProperty = new ReactiveProperty<int>(InitialValue);
        }

        public bool AddStack()
        {
            if (!HasFreeSpace)
            {
                return false;
            }

            _stackCountProperty.Value++;
            return true;
        }

        public bool RemoveStack()
        {
            if (_stackCountProperty.Value <= InitialValue)
            {
                return false;
            }

            _stackCountProperty.Value--;
            return true;
        }
    }
}
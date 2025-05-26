using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Actors;

namespace EndlessHeresy.Runtime.Inventory.Actions
{
    public sealed class ComplexItemAction : ItemAction
    {
        private readonly List<ItemAction> _actions = new();
        public void Add(ItemAction action) => _actions.Add(action);
        protected override bool TryProcessInternally(IActor owner)
        {
            return _actions
                .Select(action => action.TryProcess(owner))
                .All(success => success);
        }
    }
}
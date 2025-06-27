using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudModel : IModel
    {
        public CheatsHudModel(InventoryComponent inventory)
        {
            Inventory = inventory;
        }

        public InventoryComponent Inventory { get; }
    }
}
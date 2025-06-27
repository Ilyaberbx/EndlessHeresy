using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudModel : IModel
    {
        public CheatsHudModel(InventoryComponent inventory, StatusEffectsComponent statusEffects)
        {
            Inventory = inventory;
            StatusEffects = statusEffects;
        }

        public InventoryComponent Inventory { get; }
        public StatusEffectsComponent StatusEffects { get; }
    }
}
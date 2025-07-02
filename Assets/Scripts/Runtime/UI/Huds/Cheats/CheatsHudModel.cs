using EndlessHeresy.Runtime.Affinity;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudModel : IModel
    {
        public CheatsHudModel(InventoryComponent inventory, StatusEffectsComponent statusEffects, AffinityComponent affinity)
        {
            Inventory = inventory;
            StatusEffects = statusEffects;
            Affinity = affinity;
        }

        public InventoryComponent Inventory { get; }
        public StatusEffectsComponent StatusEffects { get; }
        public AffinityComponent Affinity { get; }
    }
}
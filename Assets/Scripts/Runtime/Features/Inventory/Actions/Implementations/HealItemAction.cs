using EndlessHeresy.Runtime.Actors;

namespace EndlessHeresy.Runtime.Inventory.Actions
{
    public sealed class HealItemAction : ItemAction
    {
        private int _healAmount;

        public void SetHealAmount(int healAmount) => _healAmount = healAmount;

        protected override bool TryProcessInternally(IActor owner)
        {
            return true;
        }
    }
}
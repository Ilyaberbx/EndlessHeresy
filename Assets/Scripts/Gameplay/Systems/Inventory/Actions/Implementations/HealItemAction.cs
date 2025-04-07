using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Health;

namespace EndlessHeresy.Gameplay.Inventory.Actions
{
    public sealed class HealItemAction : ItemAction
    {
        private int _healAmount;

        public void SetHealAmount(int healAmount) => _healAmount = healAmount;

        protected override bool TryProcessInternally(IActor owner)
        {
            if (owner.TryGetComponent(out HealthComponent health))
            {
                health.Heal(_healAmount);
                return true;
            }

            return false;
        }
    }
}
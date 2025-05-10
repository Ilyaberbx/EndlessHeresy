using EndlessHeresy.Gameplay.Stats.Modifiers;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Stats
{
    public sealed class StatsHudModel : IModel
    {
        public IStatModifiersReadonly StatModifiers { get; }
        public StatsHudModel(IStatModifiersReadonly statModifiers) => StatModifiers = statModifiers;
    }
}
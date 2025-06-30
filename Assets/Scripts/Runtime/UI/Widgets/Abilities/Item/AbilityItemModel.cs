using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Widgets.Abilities.Item
{
    public class AbilityItemModel : IModel
    {
        public Ability Ability { get; }

        public AbilityItemModel(Ability ability)
        {
            Ability = ability;
        }
    }
}
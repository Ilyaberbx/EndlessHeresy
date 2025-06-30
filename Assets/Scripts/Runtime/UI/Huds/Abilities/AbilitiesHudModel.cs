using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Huds.Abilities
{
    public sealed class AbilitiesHudModel : IModel
    {
        public AbilitiesStorageComponent AbilitiesStorage { get; }

        public AbilitiesHudModel(AbilitiesStorageComponent abilitiesStorage)
        {
            AbilitiesStorage = abilitiesStorage;
        }
    }
}
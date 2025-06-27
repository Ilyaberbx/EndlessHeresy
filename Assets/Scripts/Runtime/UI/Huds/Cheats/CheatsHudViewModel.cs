using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudViewModel : BaseViewModel<CheatsHudModel>
    {
        public IReactiveCollection<ItemType> AvailableItemsProperty { get; } = new ReactiveCollection<ItemType>();
    }
}
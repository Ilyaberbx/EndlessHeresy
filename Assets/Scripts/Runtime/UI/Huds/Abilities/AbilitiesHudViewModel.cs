using System.Linq;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Abilities.Item;
using UniRx;
using Unity.VisualScripting;

namespace EndlessHeresy.Runtime.UI.Huds.Abilities
{
    public sealed class AbilitiesHudViewModel : BaseViewModel<AbilitiesHudModel>
    {
        private readonly IViewModelFactory _factory;
        public IReactiveCollection<AbilityItemViewModel> AbilitiesProperty { get; }

        public AbilitiesHudViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            AbilitiesProperty = new ReactiveCollection<AbilityItemViewModel>();
        }

        protected override void Initialize(AbilitiesHudModel model)
        {
            var abilities = model.AbilitiesStorage.Abilities;
            AbilitiesProperty.AddRange(abilities.Select(CreateViewModel));
        }

        private AbilityItemViewModel CreateViewModel(Ability temp)
        {
            return _factory.Create<AbilityItemViewModel, AbilityItemModel>(new AbilityItemModel(temp));
        }
    }
}
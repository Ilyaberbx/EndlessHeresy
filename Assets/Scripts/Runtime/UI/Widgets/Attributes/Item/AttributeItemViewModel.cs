using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes.Item
{
    public sealed class AttributeItemViewModel : BaseViewModel<AttributeItemModel>
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        public IReactiveProperty<Sprite> IconProperty { get; } = new ReactiveProperty<Sprite>();
        public IReactiveProperty<int> ValueProperty { get; } = new ReactiveProperty<int>();

        public AttributeItemViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override void Show()
        {
            var configuration = _gameplayStaticDataService.GetAttributeData(Model.Attribute.Identifier);
            IconProperty.Value = configuration.Icon;
            Model.Attribute.ValueProperty.Subscribe(OnModelValueChanged).AddTo(CompositeDisposable);
        }

        private void OnModelValueChanged(int value) => ValueProperty.Value = value;
    }
}
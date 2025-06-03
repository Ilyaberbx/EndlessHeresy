using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects.Item
{
    public sealed class StatusEffectItemViewModel : BaseViewModel<StatusEffectItemModel>
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        public IReactiveProperty<bool> IsTemporaryEnabledProperty { get; }
        public IReactiveProperty<bool> IsStackableEnabledProperty { get; }
        public IReactiveProperty<float> TemporaryProgressProperty { get; }
        public IReactiveProperty<Sprite> IconProperty { get; }
        public IReactiveProperty<int> StackProperty { get; }

        public StatusEffectItemViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            IsTemporaryEnabledProperty = new ReactiveProperty<bool>();
            IsStackableEnabledProperty = new ReactiveProperty<bool>();
            TemporaryProgressProperty = new ReactiveProperty<float>();
            IconProperty = new ReactiveProperty<Sprite>();
            StackProperty = new ReactiveProperty<int>();
        }

        protected override void Initialize(StatusEffectItemModel model)
        {
            var data = _gameplayStaticDataService.GetStatusEffectConfiguration(Model.StatusEffect.Identifier);

            IconProperty.Value = data.Icon;

            var isStackable = Model.StatusEffect.TryGet<StackableEffectComponent>(out var stackableStatusEffect);
            var isTemporary = Model.StatusEffect.TryGet<TemporaryEffectComponent>(out var temporaryStatusEffect);

            if (isStackable)
            {
                stackableStatusEffect
                    .StackCountProperty
                    .Subscribe(OnModelStackCountChanged)
                    .AddTo(CompositeDisposable);
            }

            if (isTemporary)
            {
                temporaryStatusEffect
                    .ProgressReadOnly
                    .Subscribe(OnModelProgressChanged)
                    .AddTo(CompositeDisposable);
            }

            IsTemporaryEnabledProperty.Value = isTemporary;
            IsStackableEnabledProperty.Value = isStackable;
        }

        private void OnModelStackCountChanged(int stack) => StackProperty.Value = stack;
        private void OnModelProgressChanged(float progress) => TemporaryProgressProperty.Value = progress;
    }
}
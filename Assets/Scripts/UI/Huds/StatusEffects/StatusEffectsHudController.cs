using System.Collections.Generic;
using System.Linq;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.StatusEffects;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using VContainer;

namespace EndlessHeresy.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudController : BaseController<StatusEffectsHudModel, StatusEffectsHudView>
    {
        private IGameplayStaticDataService _gameplayStaticDataService;
        private IGameUpdateService _gameUpdateService;
        private IStatusEffectsReadOnly StatusEffects => Model.StatusEffectsReadOnly;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService,
            IGameUpdateService gameUpdateService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _gameUpdateService = gameUpdateService;
        }

        protected override void Show(StatusEffectsHudModel model, StatusEffectsHudView view)
        {
            base.Show(model, view);

            StatusEffects.ActiveStatusEffects.SubscribeWithInvoke(UpdateAllStatusEffectItems);
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        protected override void Hide()
        {
            base.Hide();

            StatusEffects.ActiveStatusEffects.Unsubscribe(UpdateAllStatusEffectItems);
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void UpdateAllStatusEffectItems(Locator<StatusEffectType, IStatusEffect> effectsLocator)
        {
            UpdateAllStatusEffects(effectsLocator.GetElements());
        }

        private void OnUpdate(float deltaTime)
        {
            var statusEffects = Model.GetActiveStatusEffects().ToArray();

            for (var i = 0; i < View.ItemViews.Length; i++)
            {
                var itemView = View.ItemViews[i];

                if (i >= statusEffects.Length)
                {
                    itemView.SetActive(false);
                    continue;
                }

                var statusEffect = statusEffects[i];

                UpdateProgressItem(statusEffect, itemView);
            }
        }

        private void UpdateAllStatusEffects(IList<IStatusEffect> statusEffects)
        {
            for (var i = 0; i < View.ItemViews.Length; i++)
            {
                var itemView = View.ItemViews[i];

                if (i >= statusEffects.Count)
                {
                    itemView.SetActive(false);
                    continue;
                }

                var statusEffect = statusEffects[i];
                UpdateStatusEffect(statusEffect, itemView);
            }
        }

        private void UpdateStatusEffect(IStatusEffect statusEffect, StatusEffectItemView view)
        {
            if (!TryGetConfiguration(statusEffect, out var configuration))
            {
                return;
            }

            view.SetStackable(configuration.IsStackable);
            view.SetTemporary(configuration.IsTemporary);
            view.SetIcon(configuration.UIData.Icon);

            if (configuration.IsStackable)
            {
                UpdateStackItem(statusEffect, view);
            }

            view.SetActive(true);
        }

        private void UpdateStackItem(IStatusEffect statusEffect, StatusEffectItemView view)
        {
            if (!statusEffect.TryGet<StackableStatusEffect>(out var stackableStatusEffect))
            {
                return;
            }

            var stackCount = stackableStatusEffect.StackCount;
            view.SetStackCount(stackCount);
        }

        private void UpdateProgressItem(IStatusEffect statusEffect, StatusEffectItemView view)
        {
            if (!statusEffect.TryGet<TemporaryStatusEffect>(out var temporaryStatusEffect))
            {
                return;
            }

            var progress = temporaryStatusEffect.GetProgress();
            view.SetProgress(progress);
        }

        private bool TryGetConfiguration(IStatusEffect statusEffect, out StatusEffectConfiguration configuration)
        {
            if (statusEffect is not IdentifiedStatusEffect identified)
            {
                configuration = null;
                return false;
            }

            var identifier = identified.Identifier;
            configuration = _gameplayStaticDataService.GetStatusEffectConfiguration(identifier);
            return true;
        }
    }
}
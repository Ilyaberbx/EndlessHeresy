using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Data.Static;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.ViewComponents;
using VContainer;

namespace EndlessHeresy.UI.Huds.Abilities
{
    public sealed class AbilitiesHudController : BaseController<AbilitiesHudModel, AbilitiesHudView>
    {
        private IGameUpdateService _gameUpdateService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private HeroConfiguration _heroConfiguration;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService,
            IGameUpdateService gameUpdateService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _gameUpdateService = gameUpdateService;
        }

        protected override void Show(AbilitiesHudModel model, AbilitiesHudView view)
        {
            base.Show(model, view);

            _heroConfiguration = _gameplayStaticDataService.HeroConfiguration;
            Model.Abilities.Subscribe(OnAbilitiesChanged);
            Model.Abilities.SetDirty();
        }

        protected override void Hide()
        {
            base.Hide();

            Model.Abilities.Unsubscribe(OnAbilitiesChanged);

            foreach (var ability in Model.Abilities.Value)
            {
                ability.State.Unsubscribe(OnAbilityStateChanged);
            }

            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            UpdateItemsCooldownText();
        }

        private void UpdateItemsCooldownText()
        {
            var abilities = Model.Abilities.Value.ToArray();

            for (var i = 0; i < View.ItemViews.Count(); i++)
            {
                var itemView = View.ItemViews.ElementAt(i);

                if (abilities.Count() <= i)
                {
                    continue;
                }

                var ability = abilities.ElementAtOrDefault(i);

                if (ability is not AbilityWithCooldown abilityWithCooldown)
                {
                    continue;
                }

                if (abilityWithCooldown.State.Value != AbilityState.Cooldown)
                {
                    continue;
                }

                itemView.SetCooldown(abilityWithCooldown.CurrentCooldownValue, abilityWithCooldown.MaxCooldown);
            }
        }

        private void OnAbilitiesChanged(IEnumerable<Ability> abilities)
        {
            UpdateItemViews();

            foreach (var ability in abilities)
            {
                ability.State.Subscribe(OnAbilityStateChanged);
            }
        }

        private void OnAbilityStateChanged(AbilityState state)
        {
            UpdateItemViews();

            if (state == AbilityState.Cooldown)
            {
                _gameUpdateService.OnUpdate += OnUpdate;
            }
        }

        private void UpdateItemViews()
        {
            var abilities = Model.Abilities.Value.ToArray();
            for (var i = 0; i < View.ItemViews.Count(); i++)
            {
                var itemView = View.ItemViews.ElementAt(i);

                if (abilities.Count() <= i)
                {
                    itemView.SetActive(false);
                    continue;
                }

                var itemData = abilities[i];
                UpdateItemView(itemView, itemData);
            }
        }

        private void UpdateItemView(AbilityItemView itemView, Ability ability)
        {
            var type = ability.Type;
            var data = _heroConfiguration.AbilityConfigurations.FirstOrDefault(x => x.Type == type);
            var icon = data?.Icon;

            itemView.SetIcon(icon);
            itemView.SetState(ability.State.Value);
            itemView.SetActive(true);
        }
    }
}
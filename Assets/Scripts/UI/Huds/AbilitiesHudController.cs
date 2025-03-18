using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Global.Services.Sprites;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using VContainer;

namespace EndlessHeresy.UI.Huds
{
    public sealed class AbilitiesHudController : BaseController<AbilitiesHudModel, AbilitiesHudView>
    {
        private ISpritesService _spritesService;
        private IGameUpdateService _gameUpdateService;

        [Inject]
        public void Construct(ISpritesService spritesService, IGameUpdateService gameUpdateService)
        {
            _spritesService = spritesService;
            _gameUpdateService = gameUpdateService;
        }

        protected override void Show(AbilitiesHudModel model, AbilitiesHudView view)
        {
            base.Show(model, view);

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
            var abilitiesInCooldown = Model.GetAbilitiesByState(AbilityState.Cooldown).ToArray();

            for (var i = 0; i < View.ItemViews.Count(); i++)
            {
                var itemView = View.ItemViews.ElementAt(i);

                if (abilitiesInCooldown.Length <= i)
                {
                    itemView.SetActive(false);
                    continue;
                }

                var ability = abilitiesInCooldown.ElementAtOrDefault(i);
                if (ability is not AbilityWithCooldown abilityWithCooldown)
                {
                    return;
                }

                itemView.SetCooldown(abilityWithCooldown.CurrentCooldownValue, abilityWithCooldown.MaxCooldown);
            }
        }

        private void OnAbilitiesChanged(IEnumerable<Ability> abilities)
        {
            foreach (var ability in abilities)
            {
                ability.State.Subscribe(OnAbilityStateChanged);
            }
        }

        private void OnAbilityStateChanged(AbilityState state)
        {
            UpdateItemViewsAsync().Forget();

            if (state == AbilityState.Cooldown)
            {
                _gameUpdateService.OnUpdate += OnUpdate;
            }
        }

        private Task UpdateItemViewsAsync()
        {
            var tasks = new List<Task>();
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
                var initializationTask = InitializeItemViewAsync(itemView, itemData);
                tasks.Add(initializationTask);
            }

            return Task.WhenAll(tasks);
        }

        private async Task InitializeItemViewAsync(AbilityItemView itemView, Ability ability)
        {
            var type = ability.Type;
            var icon = await _spritesService.GetAbilitySpriteAsync(type);
            itemView.SetState(ability.State.Value);
            itemView.SetIcon(icon);
            itemView.SetActive(true);
        }
    }
}
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Services.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityWithCooldown : Ability
    {
        private IGameUpdateService UpdateService { get; set; }
        public float CurrentCooldownValue { get; private set; }
        public float MaxCooldown { get; private set; }

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService) => UpdateService = gameUpdateService;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            State.Subscribe(OnStateChanged);
        }

        public override void Dispose()
        {
            base.Dispose();
            State.Unsubscribe(OnStateChanged);
            UpdateService.OnUpdate -= OnUpdate;
        }

        private void OnStateChanged(AbilityState state)
        {
            if (state == AbilityState.Cooldown)
            {
                UpdateService.OnUpdate += OnUpdate;
                return;
            }

            UpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            TickCooldown(deltaTime);
        }

        private void TickCooldown(float deltaTime)
        {
            CurrentCooldownValue -= deltaTime;
            CurrentCooldownValue = Mathf.Clamp(CurrentCooldownValue, 0f, MaxCooldown);

            if (CurrentCooldownValue <= 0)
            {
                SetState(AbilityState.Ready);
            }
        }

        public void SetCooldown(float seconds) => MaxCooldown = seconds;
    }
}
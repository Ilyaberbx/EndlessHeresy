using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities
{
    public abstract class AbilityWithCooldown : Ability
    {
        protected IGameUpdateService UpdateService { get; private set; }
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
            UpdateService.OnUpdate -= OnCooldownUpdate;
        }

        private void OnStateChanged(AbilityState state)
        {
            if (state == AbilityState.Cooldown)
            {
                CurrentCooldownValue = MaxCooldown;
                UpdateService.OnUpdate += OnCooldownUpdate;
                return;
            }

            UpdateService.OnUpdate -= OnCooldownUpdate;
        }

        private void OnCooldownUpdate(float deltaTime)
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
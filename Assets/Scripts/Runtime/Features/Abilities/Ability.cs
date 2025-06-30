using EndlessHeresy.Runtime.Data.Identifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class Ability
    {
        private ReactiveProperty<AbilityState> _state;
        private readonly ReactiveProperty<float> _elapsedCooldownTime;
        public AbilityType Identifier { get; private set; }
        public IReadOnlyReactiveProperty<AbilityState> State { get; private set; }
        public IReadOnlyReactiveProperty<float> ElapsedCooldownTime { get; private set; }
        public float Cooldown { get; private set; }
        public bool HasCooldown => Cooldown != 0;

        public Ability()
        {
            _elapsedCooldownTime = new ReactiveProperty<float>();
            ElapsedCooldownTime = _elapsedCooldownTime.ToReadOnlyReactiveProperty();
        }

        public void WithIdentifier(AbilityType identifier)
        {
            Identifier = identifier;
        }

        public void WithInitialState(AbilityState state)
        {
            _state = new ReactiveProperty<AbilityState>(state);
            State = _state.ToReadOnlyReactiveProperty();
        }

        public void WithCooldown(float cooldown)
        {
            Cooldown = cooldown;
        }

        public void SetState(AbilityState state)
        {
            _state.Value = state;

            if (state == AbilityState.Cooldown)
            {
                _elapsedCooldownTime.Value = 0;
            }
        }

        public void TickCooldown(float deltaTime)
        {
            _elapsedCooldownTime.Value += deltaTime;

            if (_elapsedCooldownTime.Value >= Cooldown)
            {
                SetState(AbilityState.Ready);
                _elapsedCooldownTime.Value = 0;
            }
        }

        public bool IsReady()
        {
            return State.Value == AbilityState.Ready;
        }

        public bool IsInUse()
        {
            return State.Value == AbilityState.InUse;
        }
    }
}
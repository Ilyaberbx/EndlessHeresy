using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class Ability
    {
        private ReactiveProperty<AbilityState> _state;
        private float _cooldown;
        private float _elapsedTime;
        public AbilityType Identifier { get; private set; }
        public IReadOnlyReactiveProperty<AbilityState> State { get; private set; }
        public bool HasCooldown => _cooldown != 0;

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
            _cooldown = cooldown;
        }

        public void SetState(AbilityState state)
        {
            _state.Value = state;

            if (state == AbilityState.Cooldown)
            {
                _elapsedTime = 0;
            }
        }

        public void TickCooldown(float deltaTime)
        {
            _elapsedTime += deltaTime;

            if (_elapsedTime >= _cooldown)
            {
                SetState(AbilityState.Ready);
                _elapsedTime = 0;
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
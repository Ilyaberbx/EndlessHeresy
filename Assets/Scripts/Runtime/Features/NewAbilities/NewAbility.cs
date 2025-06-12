using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UniRx;

namespace EndlessHeresy.Runtime.NewAbilities
{
    public sealed class NewAbility
    {
        private ReactiveProperty<AbilityState> _state;
        private float _cooldown;
        private float _elapsedTime;
        public AbilityType Identifier { get; private set; }
        public IReadOnlyReactiveProperty<AbilityState> State { get; private set; }
        public SequenceNode RootNode { get; private set; }

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

        public void WithRootNode(SequenceNode rootNode)
        {
            RootNode = rootNode;
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
            }
        }

        public bool IsReady()
        {
            return State.Value == AbilityState.Ready;
        }
    }
}
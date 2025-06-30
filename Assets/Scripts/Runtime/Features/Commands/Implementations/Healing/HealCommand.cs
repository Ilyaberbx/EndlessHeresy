using EndlessHeresy.Runtime.Health;

namespace EndlessHeresy.Runtime.Commands.Healing
{
    public sealed class HealCommand : IActorCommand
    {
        private readonly float _value;
        private IActor _actor;

        public HealCommand(float value)
        {
            _value = value;
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<HealthComponent>(out var health))
            {
                return;
            }
            
            health.Heal(_value);
        }
    }
}
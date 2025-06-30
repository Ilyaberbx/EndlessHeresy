using EndlessHeresy.Runtime.Commands.Healing;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;

namespace EndlessHeresy.Runtime.Commands.Damage
{
    public sealed class ReceiveDamageCommand : IActorCommand, IUndoableCommand
    {
        private readonly DamageData _data;
        private IActor _actor;

        public ReceiveDamageCommand(DamageData data)
        {
            _data = data;
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

            health.TakeDamage(_data);
        }

        public ICommand GetUndoCommand()
        {
            var value = _data.Value;
            return new HealCommand(value);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }
    }
}
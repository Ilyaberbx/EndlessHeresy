using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Health;

namespace EndlessHeresy.Runtime.Commands.Attack
{
    public sealed class DealHealing : ICommand
    {
        private readonly float _value;

        public DealHealing(float value)
        {
            _value = value;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var health = actor.GetComponent<HealthComponent>();
            health.Heal(_value);
            return Task.CompletedTask;
        }
    }
}
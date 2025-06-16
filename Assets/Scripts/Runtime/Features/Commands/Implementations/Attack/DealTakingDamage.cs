using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;

namespace EndlessHeresy.Runtime.Commands.Attack
{
    public sealed class DealTakingDamage : ICommand
    {
        private readonly DamageData _data;

        public DealTakingDamage(DamageData data)
        {
            _data = data;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var health = actor.GetComponent<HealthComponent>();
            health.TakeDamage(_data);
            return Task.CompletedTask;
        }
    }
}
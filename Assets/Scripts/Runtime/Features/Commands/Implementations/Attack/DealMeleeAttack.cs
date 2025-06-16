using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Utilities;

namespace EndlessHeresy.Runtime.Commands.Attack
{
    public sealed class DealMeleeAttack : ICommand
    {
        private readonly MeleeAttackData _data;
        private readonly ICommand _additionalTargetCommand;

        public DealMeleeAttack(MeleeAttackData data, ICommand additionalTargetCommand)
        {
            _data = data;
            _additionalTargetCommand = additionalTargetCommand;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var facingComponent = actor.GetComponent<FacingComponent>();
            var selfHealthComponent = actor.GetComponent<HealthComponent>();
            var ownerPosition = actor.Transform.position;
            var offSet = _data.OffSet;
            var attackPosition = ownerPosition
                .AddX(facingComponent.IsFacingRight ? offSet.x : -offSet.x)
                .AddY(offSet.y)
                .ToVector2();


            var hasAttacked = GamePhysicsUtility.TryOverlapCapsuleAll<HealthComponent>(
                _data.OverlapData,
                attackPosition,
                out var healthComponents);


            if (!hasAttacked)
            {
                return;
            }

            foreach (var healthComponent in healthComponents)
            {
                if (healthComponent == selfHealthComponent)
                {
                    continue;
                }

                var dealTakingDamageCommand = new DealTakingDamage(_data.DamageData);
                await dealTakingDamageCommand.ExecuteAsync(healthComponent.Owner, cancellationToken);

                if (healthComponent.IsDead())
                {
                    continue;
                }

                var target = healthComponent.Owner;

                var dealExplosionForceCommand = new DealExplosionForceImpulse(attackPosition, _data.ForceMultiplier);
                await dealExplosionForceCommand.ExecuteAsync(target, cancellationToken);
                await _additionalTargetCommand.ExecuteAsync(target, cancellationToken);
            }

            var facingDirectionForceCommand = new DealFacingDirectionForceImpulse(_data.ForceMultiplier);
            await facingDirectionForceCommand.ExecuteAsync(actor, cancellationToken);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Commands.Supporting.Gizmos;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Utilities;
using VContainer;

namespace EndlessHeresy.Runtime.Commands.Core
{
    public sealed class DealMeleeAttack : ICommand
    {
        private readonly IObjectResolver _resolver;
        private readonly MeleeAttackData _data;
        private readonly CommandInstaller _targetCommandInstaller;

        public DealMeleeAttack(IObjectResolver resolver, MeleeAttackData data, CommandInstaller targetCommandInstaller)
        {
            _resolver = resolver;
            _data = data;
            _targetCommandInstaller = targetCommandInstaller;
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

            var drawGizmosCommand = new DrawOverlapGizmos(attackPosition, _data.OverlapData);
            drawGizmosCommand.ExecuteAsync(actor, cancellationToken).Forget();

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

                if (target.TryGetComponent<CommandsComponent>(out var commands))
                {
                    var targetCommand = _targetCommandInstaller.GetCommand(_resolver);
                    commands.ExecuteAsParallel(targetCommand).Forget();
                }
            }

            var facingDirectionForceCommand = new DealFacingDirectionForceImpulse(_data.DragForceMultiplier);
            await facingDirectionForceCommand.ExecuteAsync(actor, cancellationToken);
        }
    }
}
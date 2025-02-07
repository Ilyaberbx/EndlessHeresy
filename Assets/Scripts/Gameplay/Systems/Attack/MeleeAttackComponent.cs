using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Helpers;

namespace EndlessHeresy.Gameplay.Attack
{
    public sealed class MeleeAttackComponent : PocoComponent
    {
        private HealthComponent _selfHealthComponent;
        private FacingComponent _facingComponent;
        private MeleeAttackView _meleeAttackView;

        private int _damage;
        private float _radius;
        private bool _hasHealth;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            Owner.TryGetComponent(out _meleeAttackView);
            Owner.TryGetComponent(out _facingComponent);
            _hasHealth = Owner.TryGetComponent(out _selfHealthComponent);
        }

        public void Setup(int damage, float radius)
        {
            _damage = damage;
            _radius = radius;
        }

        public void Attack()
        {
            var isFacingRight = _facingComponent.IsFacingRight;
            var at = _meleeAttackView.GetAttackPosition(isFacingRight);
            var success = PhysicsHelper.TryOverlapSphere<HealthComponent>(at, _radius, out var healthComponents);
            if (!success)
            {
                return;
            }

            foreach (var healthComponent in healthComponents)
            {
                if (_hasHealth)
                {
                    if (healthComponent == _selfHealthComponent)
                    {
                        continue;
                    }
                }

                healthComponent.TakeDamage(_damage);
            }
        }
    }
}
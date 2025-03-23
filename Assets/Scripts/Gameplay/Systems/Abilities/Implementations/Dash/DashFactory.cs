using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Data.Static.Abilities;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class DashFactory : AbilityFactory
    {
        private readonly IObjectResolver _container;
        private readonly DashConfiguration _configuration;

        public DashFactory(IObjectResolver container, DashConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public override Ability Create()
        {
            var dashAbility = new DashAbility();
            dashAbility.SetForce(_configuration.Force);
            dashAbility.SetCooldown(_configuration.Cooldown);
            dashAbility.SetType(_configuration.Type);
            dashAbility.SetCollisionForce(_configuration.CollisionForce);
            dashAbility.SetTrailData(_configuration.TrailData);
            dashAbility.SetTrailsRatio(_configuration.TrailsRatio);
            var isDashKeyDown = new IsKeyPressed(_configuration.KeyCode);
            _container.Inject(isDashKeyDown);
            dashAbility.SetCondition(isDashKeyDown);
            _container.Inject(dashAbility);
            return dashAbility;
        }
    }
}
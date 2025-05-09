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
            var ability = new DashAbility();
            ability.SetForce(_configuration.Force);
            ability.SetCooldown(_configuration.Cooldown);
            ability.SetType(_configuration.Type);
            ability.SetCollisionForce(_configuration.CollisionForce);
            ability.SetTrailData(_configuration.TrailData);
            ability.SetTrailsRatio(_configuration.TrailsRatio);
            var isDashKeyDown = new IsKeyPressed(_configuration.KeyCode);
            _container.Inject(isDashKeyDown);
            ability.SetCondition(isDashKeyDown);
            _container.Inject(ability);
            return ability;
        }
    }
}
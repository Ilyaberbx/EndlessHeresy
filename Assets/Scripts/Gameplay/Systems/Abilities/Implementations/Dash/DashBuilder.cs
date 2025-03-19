using EndlessHeresy.Gameplay.Conditions;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class DashBuilder : AbilityBuilder
    {
        private readonly IObjectResolver _container;
        private readonly DashConfiguration _configuration;

        public DashBuilder(IObjectResolver container, DashConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public override Ability Build()
        {
            var dashAbility = new DashAbility();
            dashAbility.SetCurve(_configuration.Curve);
            dashAbility.SetLength(_configuration.Length);
            dashAbility.SetSpeed(_configuration.Speed);
            dashAbility.SetCooldown(_configuration.Cooldown);
            dashAbility.SetType(_configuration.Type);
            var isDashKeyDown = new IsKeyPressed(_configuration.KeyCode);
            _container.Inject(isDashKeyDown);
            dashAbility.SetCondition(isDashKeyDown);
            _container.Inject(dashAbility);
            return dashAbility;
        }
    }
}
using EndlessHeresy.Gameplay.Abilities.Casters;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    public sealed class DashAbilityBuilder : AbilityBuilder
    {
        private readonly DashAbilityConfiguration _configuration;

        public DashAbilityBuilder(DashAbilityConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override Ability BuildInternally()
        {
            var dash = new DashAbility();
            dash.SetCurve(_configuration.DashCurve);
            dash.SetSpeed(_configuration.DashSpeed);
            dash.SetLength(_configuration.DashLength);
            dash.SetDamage(_configuration.DashDamage);
            dash.SetCastStarter(new ImmediateCaster());
            return dash;
        }
    }
}
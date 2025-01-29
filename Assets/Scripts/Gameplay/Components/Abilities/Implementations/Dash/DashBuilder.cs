using EndlessHeresy.Gameplay.Abilities.Casters;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    public sealed class DashBuilder : AbilityBuilder
    {
        private readonly DashConfiguration _configuration;

        public DashBuilder(DashConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override Ability BuildInternally()
        {
            var dash = new Dash();
            dash.Configure(_configuration.DashSpeed,
                _configuration.DashLength,
                _configuration.DashDamage,
                _configuration.Curve);

            dash.SetCastStarter(new ImmediateCaster());
            return dash;
        }
    }
}
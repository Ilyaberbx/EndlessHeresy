using EndlessHeresy.Gameplay.Abilities.Casters;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    public sealed class CrescentStrikeBuilder : AbilityBuilder
    {
        private readonly CrescentStrikeConfiguration _configuration;

        public CrescentStrikeBuilder(CrescentStrikeConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override Ability BuildInternally()
        {
            var crescentStrikeAbility = new CrescentStrike();
            crescentStrikeAbility.SetCastStarter(new ImmediateCaster());
            crescentStrikeAbility.Configure(_configuration.Damage, 
                _configuration.KnifeOffset, 
                _configuration.Duration,
                _configuration.Curve);
            return crescentStrikeAbility; 
        }
    }
}
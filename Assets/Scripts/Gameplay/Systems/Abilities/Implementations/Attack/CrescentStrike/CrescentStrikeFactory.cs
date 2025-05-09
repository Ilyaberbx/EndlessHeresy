using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Data.Static.Abilities;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    public sealed class CrescentStrikeFactory : AbilityFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly CrescentStrikeConfiguration _configuration;

        public CrescentStrikeFactory(IObjectResolver objectResolver, CrescentStrikeConfiguration configuration)
        {
            _objectResolver = objectResolver;
            _configuration = configuration;
        }

        public override Ability Create()
        {
            var ability = new CrescentStrikeAbility();
            var keyCondition = new IsKeyHold(_configuration.KeyCode);
            _objectResolver.Inject(keyCondition);
            _objectResolver.Inject(ability);
            ability.SetCondition(keyCondition);
            ability.SetKeyCondition(keyCondition);
            ability.SetType(_configuration.Type);
            ability.SetAttackData(_configuration.AttackData);
            ability.SetMaxSpinsCount(_configuration.MaxSpinsCount);
            ability.SetCooldown(_configuration.Cooldown);
            ability.SetChargesToBeReady(_configuration.ChargesToBeReady);
            return ability;
        }
    }
}
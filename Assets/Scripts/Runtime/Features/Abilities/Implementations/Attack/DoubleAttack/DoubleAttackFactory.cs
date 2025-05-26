using EndlessHeresy.Runtime.Data.Static.Abilities;
using EndlessHeresy.Runtime.Input.Conditions;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities.DoubleAttack
{
    public class DoubleAttackFactory : AbilityFactory
    {
        private readonly IObjectResolver _container;
        private readonly DoubleAttackConfiguration _configuration;

        public DoubleAttackFactory(IObjectResolver container, DoubleAttackConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public override Ability Create()
        {
            var ability = new DoubleAttackAbility();
            var isKeyPressed = new IsMousePressed(_configuration.MouseIndex);
            ability.SetCondition(isKeyPressed);
            ability.SetCooldown(_configuration.Cooldown);
            ability.SetFirstAttackData(_configuration.FirstAttackDto);
            ability.SetSecondAttackData(_configuration.SecondAttackDto);
            ability.SetType(_configuration.Type);
            _container.Inject(isKeyPressed);
            _container.Inject(ability);
            return ability;
        }
    }
}
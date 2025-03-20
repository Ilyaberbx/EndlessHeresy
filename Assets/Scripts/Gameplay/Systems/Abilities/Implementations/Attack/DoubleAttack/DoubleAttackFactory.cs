using EndlessHeresy.Gameplay.Conditions;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.DoubleAttack
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
            var doubleAttackAbility = new DoubleAttackAbility();
            var isKeyPressed = new IsMousePressed(_configuration.MouseIndex);
            doubleAttackAbility.SetCondition(isKeyPressed);
            doubleAttackAbility.SetCooldown(_configuration.Cooldown);
            doubleAttackAbility.SetFirstAttackData(_configuration.FirstAttackData);
            doubleAttackAbility.SetSecondAttackData(_configuration.SecondAttackData);
            doubleAttackAbility.SetType(_configuration.Type);
            _container.Inject(isKeyPressed);
            _container.Inject(doubleAttackAbility);
            return doubleAttackAbility;
        }
    }
}
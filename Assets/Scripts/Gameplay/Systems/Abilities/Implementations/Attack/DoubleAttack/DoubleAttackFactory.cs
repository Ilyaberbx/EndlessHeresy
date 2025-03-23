using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Data.Static.Abilities;
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
            doubleAttackAbility.SetFirstAttackData(_configuration.FirstAttackDto);
            doubleAttackAbility.SetSecondAttackData(_configuration.SecondAttackDto);
            doubleAttackAbility.SetType(_configuration.Type);
            _container.Inject(isKeyPressed);
            _container.Inject(doubleAttackAbility);
            return doubleAttackAbility;
        }
    }
}
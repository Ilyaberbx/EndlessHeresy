using EndlessHeresy.Gameplay.Conditions;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.SingleAttack
{
    public sealed class SingleAttackBuilder : AbilityBuilder
    {
        private readonly IObjectResolver _container;
        private readonly SingleAttackConfiguration _configuration;

        public SingleAttackBuilder(IObjectResolver container, SingleAttackConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public override Ability Build()
        {
            var singleAttackAbility = new SingleAttackAbility();
            singleAttackAbility.SetDamage(_configuration.Damage);
            singleAttackAbility.SetRadius(_configuration.Radius);
            singleAttackAbility.SetCooldown(_configuration.Cooldown);
            var isKeyPressed = new IsMousePressed(_configuration.MouseIndex);
            singleAttackAbility.SetCondition(isKeyPressed);
            _container.Inject(isKeyPressed);
            _container.Inject(singleAttackAbility);
            return singleAttackAbility;
        }
    }
}
using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Data.Static.Abilities;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.SingleAttack
{
    public sealed class SingleAttackFactory : AbilityFactory
    {
        private readonly IObjectResolver _container;
        private readonly SingleAttackConfiguration _configuration;

        public SingleAttackFactory(IObjectResolver container, SingleAttackConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public override Ability Create()
        {
            var singleAttackAbility = new SingleAttackAbility();
            singleAttackAbility.SetCooldown(_configuration.Cooldown);
            singleAttackAbility.SetType(_configuration.Type);
            singleAttackAbility.SetAttackData(_configuration.AttackData);
            var isKeyPressed = new IsMousePressed(_configuration.MouseIndex);
            singleAttackAbility.SetCondition(isKeyPressed);
            _container.Inject(isKeyPressed);
            _container.Inject(singleAttackAbility);
            return singleAttackAbility;
        }
    }
}
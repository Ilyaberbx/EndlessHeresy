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
            var ability = new SingleAttackAbility();
            ability.SetCooldown(_configuration.Cooldown);
            ability.SetType(_configuration.Type);
            ability.SetAttackData(_configuration.AttackData);
            var isKeyPressed = new IsMousePressed(_configuration.MouseIndex);
            ability.SetCondition(isKeyPressed);
            _container.Inject(isKeyPressed);
            _container.Inject(ability);
            return ability;
        }
    }
}
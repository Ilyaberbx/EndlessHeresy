using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class AbilitiesStorageComponent : PocoComponent
    {
        private IObjectResolver _container;
        private readonly List<Ability> _abilities = new();
        private AbilityConfiguration[] _abilityConfiguration;

        public List<Ability> Abilities => _abilities;

        [Inject]
        public void Construct(IObjectResolver container) => _container = container;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var abilityConfiguration in _abilityConfiguration)
            {
                var builder = abilityConfiguration.GetBuilder(_container);
                var ability = builder.Build();
                ability.Initialize(Owner);
                _abilities.Add(ability);
            }

            return Task.CompletedTask;
        }

        public void SetAbilities(AbilityConfiguration[] abilityConfigurations) =>
            _abilityConfiguration = abilityConfigurations;

        public bool TryGetAbility<TAbility>(out TAbility ability) where TAbility : Ability
        {
            var derivedAbility = _abilities.FirstOrDefault(temp => temp.GetType() == typeof(TAbility));

            if (derivedAbility == null)
            {
                ability = null;
                return false;
            }

            if (derivedAbility is TAbility castedAbility)
            {
                ability = castedAbility;
                return true;
            }

            ability = null;
            return false;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var ability in _abilities)
            {
                ability.Dispose();
            }
        }
    }
}
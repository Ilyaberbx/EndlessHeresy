using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Actors;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesStorageComponent : PocoComponent
    {
        private AbilityConfiguration[] _abilityConfiguration;
        private readonly List<Ability> _abilities = new();

        private IObjectResolver _resolver;

        public List<Ability> Abilities => _abilities;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var abilityConfiguration in _abilityConfiguration)
            {
                var factory = abilityConfiguration.GetFactory(_resolver);
                var ability = factory.Create();
                ability.Initialize(Owner);
                _abilities.Add(ability);
            }

            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var ability in _abilities)
            {
                ability.Dispose();
            }

            _abilities.Clear();
        }

        public void SetAbilities(AbilityConfiguration[] abilityConfigurations)
        {
            _abilityConfiguration = abilityConfigurations;
        }

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
    }
}
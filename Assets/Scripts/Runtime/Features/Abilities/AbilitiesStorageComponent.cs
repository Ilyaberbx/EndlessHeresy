using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesStorageComponent : PocoComponent
    {
        private readonly AbilityConfiguration[] _abilityConfigurations;
        private readonly ReactiveCollection<Ability> _abilities = new();
        private readonly IObjectResolver _resolver;
        public IReadOnlyReactiveCollection<Ability> Abilities => _abilities;

        public AbilitiesStorageComponent(IObjectResolver resolver, AbilityConfiguration[] abilityConfigurations)
        {
            _resolver = resolver;
            _abilityConfigurations = abilityConfigurations;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var abilityConfiguration in _abilityConfigurations)
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
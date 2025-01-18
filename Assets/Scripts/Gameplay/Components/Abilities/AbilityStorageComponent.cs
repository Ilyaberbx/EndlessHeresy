using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class AbilityStorageComponent : PocoComponent
    {
        private AbilityConfiguration[] _abilityConfigurations;
        private readonly List<Ability> _abilities = new();

        public IReadOnlyList<Ability> Abilities => _abilities;

        public void Configure(AbilityConfiguration[] abilityConfigurations) =>
            _abilityConfigurations = abilityConfigurations;

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            _abilities.Clear();

            if (_abilityConfigurations.IsNullOrEmpty())
            {
                return;
            }

            foreach (var configuration in _abilityConfigurations)
            {
                var builder = configuration.GetBuilder();
                var ability = builder.Build();
                _abilities.Add(ability);
            }
        }
    }
}
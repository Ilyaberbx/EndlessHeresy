using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.NewAbilities
{
    public sealed class AbilitiesCastComponent : PocoComponent
    {
        private AbilitiesComponent _storage;
        private IEnumerable<Ability> Abilities => _storage.Abilities;
        private Task _castTask;
        private Ability _activeAbility;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesComponent>();
            return Task.CompletedTask;
        }

        public bool HasActiveAbility() => _activeAbility != null;

        public bool TryCast<TAbility>() where TAbility : Ability
        {
            if (Abilities.IsNullOrEmpty())
            {
                return false;
            }

            if (HasActiveAbility())
            {
                return false;
            }

            var ability = Abilities.FirstOrDefault(temp => temp.GetType() == typeof(TAbility));

            if (ability?.State.Value is not AbilityState.Ready)
            {
                return false;
            }

            CastAsync(ability).Forget();
            return true;
        }

        private Task CastAsync(Ability ability)
        {
            throw new System.NotImplementedException();
        }
    }
}
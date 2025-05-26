using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesCastComponent : PocoComponent
    {
        private AbilitiesStorageComponent _storage;
        private IEnumerable<Ability> Abilities => _storage.Abilities;
        private Task _castTask;
        private Ability _activeAbility;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
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

            var castCondition = ability.Condition;

            if (!castCondition.SafeInvoke())
            {
                return false;
            }

            CastAsync(ability).Forget();
            return true;
        }

        private async Task CastAsync(Ability ability)
        {
            _activeAbility = ability;
            await ability.UseAsync(Owner.DestroyCancellationToken);
            _activeAbility = null;
        }
    }
}
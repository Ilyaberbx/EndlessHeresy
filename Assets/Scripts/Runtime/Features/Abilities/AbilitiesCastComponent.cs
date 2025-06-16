using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesCastComponent : PocoComponent
    {
        private AbilitiesStorageComponent _storage;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            return Task.CompletedTask;
        }

        public async Task<bool> TryCastAsync(AbilityType identifier)
        {
            if (HasActiveAbilities())
            {
                return false;
            }

            var ability = _storage.Abilities.FirstOrDefault(temp => temp.Identifier == identifier);
            if (ability == null)
            {
                return false;
            }

            if (!ability.IsReady())
            {
                return false;
            }

            ability.SetState(AbilityState.InUse);
            await ability.RootCommand.ExecuteAsync(Owner, DisposalToken);
            ability.SetState(AbilityState.Cooldown);
            return true;
        }

        private bool HasActiveAbilities()
        {
            return _storage.Abilities.Any(temp => temp.State.Value == AbilityState.InUse);
        }
    }
}
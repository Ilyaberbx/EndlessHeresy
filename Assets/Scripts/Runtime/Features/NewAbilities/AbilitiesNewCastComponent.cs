using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.NewAbilities
{
    public sealed class AbilitiesNewCastComponent : PocoComponent
    {
        private AbilitiesNewStorageComponent _storage;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesNewStorageComponent>();
            return Task.CompletedTask;
        }

        public async Task<bool> TryCast(AbilityType identifier)
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
            var context = new AbilityContext(Owner, ability);
            await ability.RootNode.ExecuteAsync(context, DisposalToken);
            ability.SetState(AbilityState.Cooldown);
            return true;
        }

        private bool HasActiveAbilities()
        {
            return _storage.Abilities.Any(temp => temp.State.Value == AbilityState.InUse);
        }
    }
}
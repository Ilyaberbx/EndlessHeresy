using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Tick;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesCastComponent : PocoComponent
    {
        private readonly IGameUpdateService _gameUpdateService;
        private AbilitiesStorageComponent _storage;

        public AbilitiesCastComponent(IGameUpdateService gameUpdateService)
        {
            _gameUpdateService = gameUpdateService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _gameUpdateService.OnUpdate += OnUpdate;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
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

        private void OnUpdate(float deltaTime)
        {
            foreach (var ability in _storage.Abilities)
            {
                if (ability.State.Value == AbilityState.Cooldown)
                {
                    ability.TickCooldown(deltaTime);
                }
            }
        }

        private bool HasActiveAbilities()
        {
            return _storage.Abilities.Any(temp => temp.State.Value == AbilityState.InUse);
        }
    }
}
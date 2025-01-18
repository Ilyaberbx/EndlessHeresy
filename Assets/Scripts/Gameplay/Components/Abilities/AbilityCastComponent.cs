using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Global.Services.Input;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class AbilityCastComponent : PocoComponent
    {
        private AbilityStorageComponent _abilityStorage;
        private InputService _inputService;
        private GameUpdateService _gameUpdateService;

        private IReadOnlyList<Ability> Abilities => _abilityStorage.Abilities;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            if (Owner.TryGetComponent<AbilityStorageComponent>(out var abilityStorageComponent))
            {
                _abilityStorage = abilityStorageComponent;
            }
            else
            {
                return;
            }

            _inputService = ServiceLocator.Get<InputService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();

            _gameUpdateService.OnUpdate += OnUpdate;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            if (Abilities.IsNullOrEmpty())
            {
                return;
            }

            foreach (var ability in Abilities)
            {
                var isHotkeyPressed = _inputService.GetKeyDown(ability.HotKey);
                if (!isHotkeyPressed)
                {
                    continue;
                }

                if (ability.Status is not AbilityStatus.Ready)
                {
                    continue;
                }

                ability.StartCast(Owner);
            }
        }
    }
}
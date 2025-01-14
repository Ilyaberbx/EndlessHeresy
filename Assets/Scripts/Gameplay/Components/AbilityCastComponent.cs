using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Services.Update;
using EndlessHeresy.Global.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay
{
    [RequireComponent(typeof(AbilityStorageComponent))]
    public sealed class AbilityCastComponent : MonoBehaviour, IGameUpdatable
    {
        [SerializeField] private GameObject _owner;

        private AbilityStorageComponent _abilityStorage;
        private InputService _inputService;
        private GameUpdateService _gameUpdateService;

        private IReadOnlyList<Ability> Abilities => _abilityStorage.Abilities;

        private void Start()
        {
            _abilityStorage = GetComponent<AbilityStorageComponent>();
            _abilityStorage.Initialize();
            _inputService = ServiceLocator.Get<InputService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();

            _gameUpdateService.Subscribe(this);
        }

        private void OnDestroy()
        {
            _gameUpdateService.Unsubscribe(this);
        }

        public void OnUpdate(float deltaTime)
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

                ability.StartCast(_owner);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Static.Components.Controls;
using EndlessHeresy.Runtime.Services.Tick;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesControlsHandler : PocoComponent
    {
        private readonly IGameUpdateService _gameUpdateService;
        private readonly AbilityControlsData[] _data;
        private AbilitiesStorageComponent _storage;
        private AbilitiesCastComponent _cast;

        public AbilitiesControlsHandler(IGameUpdateService gameUpdateService, AbilityControlsData[] data)
        {
            _gameUpdateService = gameUpdateService;
            _data = data;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _cast = Owner.GetComponent<AbilitiesCastComponent>();
            _gameUpdateService.OnUpdate += OnUpdate;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            if (_storage.Abilities.Any(temp => temp.IsInUse()))
            {
                return;
            }

            foreach (var data in _data)
            {
                var keyCode = data.KeyCode;

                if (UnityEngine.Input.GetKeyDown(keyCode))
                {
                    _cast.TryCastAsync(data.AbilityIdentifier).Forget();
                }
            }
        }
    }
}
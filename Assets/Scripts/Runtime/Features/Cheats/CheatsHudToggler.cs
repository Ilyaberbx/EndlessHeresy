using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Affinity;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Huds.Cheats;
using EndlessHeresy.Runtime.UI.Services.Cheats;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Cheats
{
    public sealed class CheatsHudToggler : PocoComponent
    {
        private readonly ICheatsService _cheatsService;
        private readonly InputAction _inputAction;

        private InventoryComponent _inventory;
        private StatusEffectsComponent _statusEffects;
        private AffinityComponent _affinity;
        private bool _isCheatsEnabled;

        public CheatsHudToggler(ICheatsService cheatsService, InputAction inputAction)
        {
            _cheatsService = cheatsService;
            _inputAction = inputAction;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _inventory = Owner.GetComponent<InventoryComponent>();
            _statusEffects = Owner.GetComponent<StatusEffectsComponent>();
            _affinity = Owner.GetComponent<AffinityComponent>();
            _inputAction.performed += OnActionPerformed;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _inputAction.performed -= OnActionPerformed;
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            _isCheatsEnabled = !_isCheatsEnabled;

            if (_isCheatsEnabled)
            {
                _cheatsService.Show(new CheatsHudModel(_inventory, _statusEffects, _affinity));
            }
            else
            {
                _cheatsService.Hide();
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Modals.Inventory;
using EndlessHeresy.Runtime.UI.Services.Modals;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Inventory
{
    public sealed class InventoryModalToggler : PocoComponent
    {
        private readonly IModalsService _modalsService;
        private readonly InputAction _inputAction;
        private bool _isOpen;
        private InventoryComponent _inventory;

        public InventoryModalToggler(IModalsService modalsService, InputAction inputAction)
        {
            _modalsService = modalsService;
            _inputAction = inputAction;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _inventory = Owner.GetComponent<InventoryComponent>();
            _inputAction.performed += OnActionPerformed;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _inputAction.performed -= OnActionPerformed;
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            _isOpen = !_isOpen;

            if (!_isOpen)
            {
                _modalsService.HideAllOfType<InventoryModalViewModel>();
                return;
            }

            var model = new InventoryModalModel(_inventory);
            _modalsService.ShowAsync<InventoryModalViewModel, InventoryModalModel>(model).Forget();
        }
    }
}
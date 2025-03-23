using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.Input;
using VContainer;

namespace EndlessHeresy.Gameplay.Facing
{
    public sealed class MouseFacingComponent : PocoComponent
    {
        private ICameraService _cameraService;
        private IInputService _inputService;

        private FacingComponent _facingComponent;

        public FacingComponent FacingComponent => _facingComponent;

        [Inject]
        public void Construct(ICameraService cameraService, IInputService inputService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _facingComponent = Owner.GetComponent<FacingComponent>();
            return base.OnPostInitializeAsync(cancellationToken);
        }

        public void UpdateFacing()
        {
            if (_facingComponent.IsLocked)
            {
                return;
            }

            var isMouseOnRightSide = CheckMouseOnRightSide();
            _facingComponent.Face(GetType(), isMouseOnRightSide);
        }

        private bool CheckMouseOnRightSide()
        {
            var camera = _cameraService.MainCamera;
            if (camera == null)
            {
                return false;
            }

            var mouseWorldPosition = camera.ScreenToWorldPoint(_inputService.GetMousePosition());
            var ownerPosition = Owner.Transform.position;

            return mouseWorldPosition.x > ownerPosition.x;
        }
    }
}
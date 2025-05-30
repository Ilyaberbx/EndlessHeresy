using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Services.Camera;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.UI.World
{
    [RequireComponent(typeof(Canvas))]
    public sealed class WorldCanvasView : MonoComponent
    {
        private ICameraService _cameraService;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        [Inject]
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _canvas.worldCamera = _cameraService.MainCamera;
            return Task.CompletedTask;
        }
    }
}
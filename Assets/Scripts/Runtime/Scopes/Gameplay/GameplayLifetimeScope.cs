using EndlessHeresy.Runtime.FloatingMessages.Factory;
using EndlessHeresy.Runtime.Services.Camera;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using EndlessHeresy.Runtime.Services.Gameplay.Factory;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Gameplay
{
    public sealed class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCameraBase _followCamera;
        [SerializeField] private Transform _floatingMessageContainer;
        [SerializeField] private Transform _dummiesSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayEntryPoint>()
                .WithParameter(_dummiesSpawnPoint);
            builder.RegisterEntryPoint<GameplayStaticDataService>();
            builder.RegisterEntryPoint<GameplayFactoryService>();
            builder.Register<ICameraService, CameraService>(Lifetime.Singleton)
                .WithParameter(_camera)
                .WithParameter(_followCamera);
            builder.Register<FloatingMessagesFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(_floatingMessageContainer);
            builder.Register<FloatingMessagesService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IViewModelFactory, ViewModelFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<UpdateViewModelsFactoryService>();
        }
    }
}
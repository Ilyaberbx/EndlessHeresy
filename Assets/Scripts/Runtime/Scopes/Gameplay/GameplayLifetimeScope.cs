using Cinemachine;
using EndlessHeresy.Runtime.FloatingMessages.Factory;
using EndlessHeresy.Runtime.Services.Camera;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using EndlessHeresy.Runtime.Services.Gameplay.Factory;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Services.Input;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayEntryPoint>();
            builder.RegisterEntryPoint<GameplayStaticDataService>();
            builder.RegisterEntryPoint<GameplayFactoryService>();
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<ICameraService, CameraService>(Lifetime.Singleton)
                .WithParameter(_camera)
                .WithParameter(_followCamera);
            builder.Register<FloatingMessagesFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(_floatingMessageContainer);
            builder.Register<FloatingMessagesService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IViewModelFactory, ViewModelFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<ScopedViewModelsFactoryService>();
        }
    }
}
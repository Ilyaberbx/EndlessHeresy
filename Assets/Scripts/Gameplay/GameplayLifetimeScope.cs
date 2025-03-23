using Cinemachine;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.Input;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.Services.FloatingMessages;
using EndlessHeresy.UI.Services.FloatingMessages.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
{
    public sealed class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCameraBase _followCamera;
        [SerializeField] private Transform _floatingMessageContainer;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayEntryPoint>();
            builder.Register<IGameplayFactoryService, GameplayFactoryService>(Lifetime.Singleton);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<GameplayStaticDataService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ICameraService, CameraService>(Lifetime.Singleton)
                .WithParameter(_camera)
                .WithParameter(_followCamera);
            builder.Register<FloatingMessagesFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(_floatingMessageContainer);
            builder.Register<FloatingMessagesService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
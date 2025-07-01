using EndlessHeresy.Runtime.Scopes.Global.States.Factory;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.Services.Global.States;
using EndlessHeresy.Runtime.Services.Input;
using EndlessHeresy.Runtime.Services.Randomization;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Global
{
    public sealed class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerInput _input;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GlobalEntryPoint>();
            builder.RegisterEntryPoint<InputService>()
                .WithParameter(_input);
            builder.Register<IGameStatesFactory, GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStatesService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameUpdateService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IAssetsService, ResourcesService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<RandomizationService>();
        }
    }
}
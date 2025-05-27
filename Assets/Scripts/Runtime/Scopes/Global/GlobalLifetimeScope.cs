using EndlessHeresy.Runtime.Scopes.Global.States;
using EndlessHeresy.Runtime.Scopes.Global.States.Factory;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Services.Global.States;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Global
{
    public sealed class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform _hudsRoot;
        [SerializeField] private Transform _modalsRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GlobalEntryPoint>();
            builder.Register<IGameStatesFactory, GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStatesService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameUpdateService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IAssetsService, ResourcesService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayStaticDataService>();
        }
    }
}
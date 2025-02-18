using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Global.Services.AssetsManagement;
using EndlessHeresy.Global.Services.StatesManagement;
using EndlessHeresy.Global.States.Factory;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Global
{
    public sealed class GlobalLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GlobalEntryPoint>();
            builder.Register<IGameStatesFactory, GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStatesService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameUpdateService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IAssetsService, ResourcesService>(Lifetime.Singleton);
        }
    }
}
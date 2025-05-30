using EndlessHeresy.Runtime.UI.Services.Huds;
using EndlessHeresy.Runtime.UI.Services.Modals;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.UI
{
    public sealed class UILifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform _hudsRoot;
        [SerializeField] private Transform _modalsRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IHudsService, HudsService>(Lifetime.Singleton).WithParameter(_hudsRoot);
            builder.Register<IModalsService, ModalsService>(Lifetime.Singleton).WithParameter(_modalsRoot);
        }
    }
}
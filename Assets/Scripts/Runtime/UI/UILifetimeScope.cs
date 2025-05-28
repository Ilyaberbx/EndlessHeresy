using EndlessHeresy.Runtime.UI.Services.Huds;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.UI
{
    public sealed class UILifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform _hudsRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IHudsService, HudsService>(Lifetime.Singleton).WithParameter(_hudsRoot);
        }
    }
}
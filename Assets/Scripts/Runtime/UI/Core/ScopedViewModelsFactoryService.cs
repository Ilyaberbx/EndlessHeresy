using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Services.Huds;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.UI.Core
{
    public sealed class ScopedViewModelsFactoryService : IInitializable
    {
        private readonly IViewModelFactory _factory;
        private readonly IHudsService _hudsService;

        public ScopedViewModelsFactoryService(IViewModelFactory factory, IHudsService hudsService)
        {
            _factory = factory;
            _hudsService = hudsService;
        }

        public void Initialize()
        {
            _hudsService.UpdateFactory(_factory);
        }
    }
}
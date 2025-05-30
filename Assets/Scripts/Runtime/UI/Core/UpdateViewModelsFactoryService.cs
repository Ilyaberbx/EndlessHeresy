using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Services.Huds;
using EndlessHeresy.Runtime.UI.Services.Modals;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.UI.Core
{
    public sealed class UpdateViewModelsFactoryService : IInitializable
    {
        private readonly IViewModelFactory _factory;
        private readonly IHudsService _hudsService;
        private readonly IModalsService _modalsService;

        public UpdateViewModelsFactoryService(IViewModelFactory factory,
            IHudsService hudsService,
            IModalsService modalsService)
        {
            _factory = factory;
            _hudsService = hudsService;
            _modalsService = modalsService;
        }

        public void Initialize()
        {
            _hudsService.UpdateFactory(_factory);
            _modalsService.UpdateFactory(_factory);
        }
    }
}
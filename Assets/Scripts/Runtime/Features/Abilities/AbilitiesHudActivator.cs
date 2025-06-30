using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Huds.Abilities;
using EndlessHeresy.Runtime.UI.Services.Huds;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesHudActivator : PocoComponent
    {
        private readonly IHudsService _hudsService;

        public AbilitiesHudActivator(IHudsService hudsService)
        {
            _hudsService = hudsService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var abilitiesStorage = Owner.GetComponent<AbilitiesStorageComponent>();
            return _hudsService.ShowAsync<AbilitiesHudViewModel, AbilitiesHudModel>(
                new AbilitiesHudModel(abilitiesStorage), ShowType.Additive);
        }
    }
}
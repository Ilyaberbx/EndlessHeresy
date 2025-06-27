using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Huds.Cheats;

namespace EndlessHeresy.Runtime.UI.Services.Cheats
{
    public interface ICheatsService
    {
        public void Show(CheatsHudModel model);
        void Hide();
        void UpdateFactory(IViewModelFactory factory);
    }
}
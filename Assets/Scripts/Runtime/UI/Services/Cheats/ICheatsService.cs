using EndlessHeresy.Runtime.UI.Core.Factory;

namespace EndlessHeresy.Runtime.UI.Services.Cheats
{
    public interface ICheatsService
    {
        void Show();
        void Hide();
        void UpdateFactory(IViewModelFactory factory);
    }
}
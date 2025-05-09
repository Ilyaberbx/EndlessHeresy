using EndlessHeresy.UI.ViewComponents;

namespace EndlessHeresy.Gameplay.Services.FloatingMessages.Factory
{
    public interface IFloatingMessagesFactory
    {
        public FloatingMessageView Create();
        public void Dispose(FloatingMessageView messageView);
    }
}
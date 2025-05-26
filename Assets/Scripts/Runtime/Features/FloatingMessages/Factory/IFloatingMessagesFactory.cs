using EndlessHeresy.Meta.UI.ViewComponents;

namespace EndlessHeresy.Runtime.FloatingMessages.Factory
{
    public interface IFloatingMessagesFactory
    {
        public FloatingMessageView Create();
        public void Dispose(FloatingMessageView messageView);
    }
}
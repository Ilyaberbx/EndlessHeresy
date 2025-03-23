using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Data.Operational;

namespace EndlessHeresy.UI.Services.FloatingMessages
{
    public interface IFloatingMessagesService
    {
        public Task ShowAsync(ShowFloatingMessageDto messageDto);
    }
}
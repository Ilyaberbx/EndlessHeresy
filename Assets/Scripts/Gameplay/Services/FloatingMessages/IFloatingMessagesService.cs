using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Data.Operational;

namespace EndlessHeresy.Gameplay.Services.FloatingMessages
{
    public interface IFloatingMessagesService
    {
        public Task ShowAsync(ShowFloatingMessageQuery messageQuery);
    }
}
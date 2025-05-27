using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Operational;

namespace EndlessHeresy.Runtime.Services.FloatingMessages
{
    public interface IFloatingMessagesService
    {
        public Task ShowAsync(ShowFloatingMessageQuery messageQuery);
    }
}
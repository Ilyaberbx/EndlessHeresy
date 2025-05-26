using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Operational;

namespace EndlessHeresy.Runtime.FloatingMessages
{
    public interface IFloatingMessagesService
    {
        public Task ShowAsync(ShowFloatingMessageQuery messageQuery);
    }
}
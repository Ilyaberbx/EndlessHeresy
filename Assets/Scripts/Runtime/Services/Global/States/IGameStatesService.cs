using System.Threading.Tasks;
using EndlessHeresy.Runtime.Scopes.Global.States;

namespace EndlessHeresy.Runtime.Services.Global.States
{
    public interface IGameStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameState, new();
    }
}
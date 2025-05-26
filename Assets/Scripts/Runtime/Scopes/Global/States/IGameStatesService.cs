using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Scopes.Global.States
{
    public interface IGameStatesService
    {
        Task ChangeStateAsync<TState>() where TState : BaseGameState, new();
    }
}
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DeadState : BaseState<HeroActor>
    {
        public override Task EnterAsync(CancellationToken token) => Task.CompletedTask;

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class HeroIdleState : HeroState
    {
        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);
            
            var animationsComponent = context.get
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
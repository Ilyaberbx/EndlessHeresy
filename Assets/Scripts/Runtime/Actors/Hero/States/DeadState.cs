using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Movement;
using EndlessHeresy.Runtime.States;
using UnityEngine;

namespace EndlessHeresy.Runtime.Actors.Hero.States
{
    public sealed class DeadState : BaseState<HeroActor>
    {
        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}
using System;
using EndlessHeresy.Runtime.Actors;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class DrawGizmosObserver : MonoComponent
    {
        public event Action OnDrawGizmosTriggered;

        private void OnDrawGizmos() => OnDrawGizmosTriggered?.Invoke();
    }
}
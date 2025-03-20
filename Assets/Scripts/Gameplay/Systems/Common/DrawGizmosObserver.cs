using System;
using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class DrawGizmosObserver : MonoComponent
    {
        public event Action OnDrawGizmosTriggered;

        private void OnDrawGizmos() => OnDrawGizmosTriggered?.Invoke();
    }
}
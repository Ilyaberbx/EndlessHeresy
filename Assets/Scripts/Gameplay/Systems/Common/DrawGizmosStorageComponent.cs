using System;
using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class DrawGizmosStorageComponent : MonoComponent
    {
        public event Action OnDrawGizmosTriggered;

        private void OnDrawGizmos() => OnDrawGizmosTriggered?.Invoke();
    }
}
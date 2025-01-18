using System;

namespace EndlessHeresy.Gameplay.Abilities.Casters
{
    public class ImmediateCaster : ICastStarter
    {
        public event Action OnCastApplied;

        public void StartCast()
        {
            OnCastApplied?.Invoke();
        }
    }
}
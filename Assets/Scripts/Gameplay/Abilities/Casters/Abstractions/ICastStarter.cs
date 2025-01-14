using System;

namespace EndlessHeresy.Gameplay.Abilities.Casters
{
    public interface ICastStarter
    {
        public event Action OnCastApplied;
        public void StartCast();
    }
}
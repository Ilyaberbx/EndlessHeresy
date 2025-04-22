using System;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IStackNotifier
    {
        public event Action<int> OnStackAdded;
    }
}
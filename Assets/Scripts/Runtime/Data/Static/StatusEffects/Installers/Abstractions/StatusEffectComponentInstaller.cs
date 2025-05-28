using System;
using EndlessHeresy.Runtime.StatusEffects.Builder;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions
{
    [Serializable]
    public abstract class StatusEffectComponentInstaller
    {
        public abstract void Install(StatusEffectsBuilder builder);
    }
}
using System;
using EndlessHeresy.Runtime.StatusEffects.Builder;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers
{
    [Serializable]
    public abstract class StatusEffectComponentInstaller
    {
        public abstract void Install(StatusEffectBuilder builder);
    }
}
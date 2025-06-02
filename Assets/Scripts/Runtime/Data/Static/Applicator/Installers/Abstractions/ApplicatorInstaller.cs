using System;
using EndlessHeresy.Runtime.Applicators;

namespace EndlessHeresy.Runtime.Data.Static.Applicator.Installers
{
    [Serializable]
    public abstract class ApplicatorInstaller
    {
        public abstract IApplicator GetApplicator();
    }
}
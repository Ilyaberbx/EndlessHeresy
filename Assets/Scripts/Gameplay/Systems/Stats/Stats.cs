using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;
using EndlessHeresy.Core.Extensions;
using EndlessHeresy.Gameplay.Stats.Abstractions;

namespace EndlessHeresy.Gameplay.Stats
{
    public sealed class Stats : IStats
    {
        private readonly ILocator<Type, BaseStat> _statsLocator;

        public BaseStat[] GetAllStats() => _statsLocator.GetElements();

        public Stats(IDictionary<Type, BaseStat> stats)
        {
            _statsLocator = stats.ToLocator();
        }

        public TStat GetStat<TStat>() where TStat : BaseStat
        {
            var statType = typeof(TStat);
            var success = _statsLocator.TryGet(statType, out BaseStat stat);

            if (!success)
            {
                DebugUtility.LogException<NullReferenceException>();
                return null;
            }

            if (stat is TStat concreteStat)
            {
                DebugUtility.LogException<TypeAccessException>();
                return concreteStat;
            }

            return null;
        }
    }
}
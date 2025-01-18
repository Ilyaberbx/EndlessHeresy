using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors.Hero;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [Serializable]
    public sealed class GameplayStaticDataService : PocoService
    {
        [SerializeField] private HeroConfiguration _heroConfiguration;

        public HeroConfiguration HeroConfiguration => _heroConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
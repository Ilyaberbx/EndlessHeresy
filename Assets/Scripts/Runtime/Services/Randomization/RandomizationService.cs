using System;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace EndlessHeresy.Runtime.Services.Randomization
{
    public sealed class RandomizationService : IInitializable
    {
        public void Initialize()
        {
            var seed = DateTime.Now.Millisecond;
            Random.InitState(seed);
        }
    }
}
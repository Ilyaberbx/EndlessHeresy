using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Behaviour
{
    public sealed class BehaviourObjectResolver : MonoBehaviour
    {
        public IObjectResolver Resolver { get; private set; }

        [Inject]
        public void Construct(IObjectResolver resolver) => Resolver = resolver;
    }
}
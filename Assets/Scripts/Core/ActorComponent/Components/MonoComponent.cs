using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Core
{
    public abstract class MonoComponent : MonoBehaviour, IComponent
    {
        public IActor Owner { get; private set; }
        public Task InitializeAsync() => OnInitializeAsync(destroyCancellationToken);
        public Task PostInitializeAsync() => OnPostInitializeAsync(destroyCancellationToken);

        public void Dispose()
        {
            OnDispose();
            Destroy(gameObject);
        }

        public void SetActor(IActor actor) => Owner = actor;
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected virtual Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private void OnDispose()
        {
        }
    }
}
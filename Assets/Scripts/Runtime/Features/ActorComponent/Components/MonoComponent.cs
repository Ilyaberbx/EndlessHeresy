using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Runtime
{
    public abstract class MonoComponent : MonoBehaviour, IComponent
    {
        public IActor Owner { get; private set; }

        public Task InitializeAsync()
        {
            return OnInitializeAsync(destroyCancellationToken);
        }

        public Task PostInitializeAsync()
        {
            return OnPostInitializeAsync(destroyCancellationToken);
        }

        public void Dispose()
        {
            OnDispose();
        }

        public void SetActor(IActor actor) => Owner = actor;
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected virtual Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected virtual void OnDispose()
        {
        }
    }
}
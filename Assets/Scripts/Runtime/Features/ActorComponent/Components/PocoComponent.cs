using System.Threading;
using System.Threading.Tasks;
using UniRx;

namespace EndlessHeresy.Runtime
{
    public abstract class PocoComponent : IComponent
    {
        private readonly CancellationTokenSource _disposeCanceller = new();
        public IActor Owner { get; private set; }
        protected CancellationToken DisposalToken => _disposeCanceller.Token;
        protected CompositeDisposable CompositeDisposable { get; } = new();

        public Task InitializeAsync()
        {
            return OnInitializeAsync(_disposeCanceller.Token);
        }

        public Task PostInitializeAsync()
        {
            return OnPostInitializeAsync(_disposeCanceller.Token);
        }

        public virtual void Dispose()
        {
            OnDispose();
            _disposeCanceller.Cancel();
            CompositeDisposable.Dispose();
        }

        public void SetActor(IActor actor) => Owner = actor;
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected virtual Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected virtual void OnDispose()
        {
        }
    }
}
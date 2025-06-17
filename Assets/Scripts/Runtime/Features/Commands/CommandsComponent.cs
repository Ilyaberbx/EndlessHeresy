using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;

namespace EndlessHeresy.Runtime.Commands
{
    public sealed class CommandsComponent : PocoComponent
    {
        private readonly Queue<QueuedCommand> _queue = new();
        private CancellationTokenSource _stopCts = new();
        private CancellationToken StopCancellationToken => _stopCts.Token;

        protected override void OnDispose()
        {
            StopAll();
        }

        public Task<bool> EnqueueInMainSequenceAsync(ICommand command)
        {
            var queued = new QueuedCommand(command);
            _queue.Enqueue(queued);

            if (_queue.Count == 1)
            {
                ProcessNextAsync().Forget();
            }

            return queued.Completion.Task;
        }

        public Task<bool> ExecuteAsParallel(ICommand command)
        {
            var queued = new QueuedCommand(command);
            ProcessConcurrentAsync(queued).Forget();
            return queued.Completion.Task;
        }

        public void StopAll()
        {
            if (!_stopCts.IsCancellationRequested)
            {
                _stopCts.Cancel();
            }

            _stopCts.Dispose();
            _stopCts = new CancellationTokenSource();
            _queue.Clear();
        }

        private async Task ProcessNextAsync()
        {
            while (_queue.TryPeek(out var current))
            {
                try
                {
                    await current.Command.ExecuteAsync(Owner, StopCancellationToken);
                    current.Completion.TrySetResult(true);
                }
                catch (OperationCanceledException)
                {
                    current.Completion.TrySetCanceled();
                }
                catch (Exception ex)
                {
                    current.Completion.TrySetException(ex);
                }

                _queue.Dequeue();
            }
        }

        private async Task ProcessConcurrentAsync(QueuedCommand queued)
        {
            try
            {
                await queued.Command.ExecuteAsync(Owner, StopCancellationToken);
                queued.Completion.TrySetResult(true);
            }
            catch (OperationCanceledException)
            {
                queued.Completion.TrySetCanceled();
            }
            catch (Exception ex)
            {
                queued.Completion.TrySetException(ex);
            }
        }
    }
}
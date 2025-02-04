using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Core.States
{
    public abstract class HierarchicalState<TContext> : BaseState<TContext> where TContext : IContext
    {
        private readonly IList<BaseState<TContext>> _children = new List<BaseState<TContext>>();

        public void AddChild(BaseState<TContext> child) => _children.Add(child);
        public void RemoveChild(BaseState<TContext> child) => _children.Remove(child);

        private bool NoChildren => _children.Count == 0;

        public override Task EnterAsync(CancellationToken token)
        {
            if (NoChildren)
            {
                return Task.CompletedTask;
            }

            var childEnterTasks = _children.Select(temp => temp.EnterAsync(token));
            return Task.WhenAll(childEnterTasks);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            if (NoChildren)
            {
                return Task.CompletedTask;
            }

            var childExitTasks = _children.Select(temp => temp.ExitAsync(token));
            return Task.WhenAll(childExitTasks);
        }
    }
}
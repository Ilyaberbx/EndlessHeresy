using System.Collections.Generic;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public abstract class CollectionNode : AbilityNode
    {
        public IList<AbilityNode> Children { get; }

        protected CollectionNode(AbilityNode[] children)
        {
            Children = children;
        }
    }
}
using System;
using System.Linq;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class AbilitiesExtensions
    {
        public static bool InsertAfter(this CollectionNode node, Type targetType, int occurrenceIndex,
            AbilityNode newNode)
        {
            if (node == null || newNode == null || targetType == null)
            {
                Debug.LogWarning("InsertAfter failed: null reference.");
                return false;
            }

            return TryInsert(node, targetType, occurrenceIndex, newNode, insertAfter: true);
        }

        public static bool InsertBefore(this CollectionNode node, Type targetType, int occurrenceIndex,
            AbilityNode newNode)
        {
            if (node == null || newNode == null || targetType == null)
            {
                Debug.LogWarning("InsertBefore failed: null reference.");
                return false;
            }

            return TryInsert(node, targetType, occurrenceIndex, newNode, insertAfter: false);
        }

        private static bool TryInsert(CollectionNode node, Type targetType, int targetIndex, AbilityNode newNode,
            bool insertAfter, ref int matchCount)
        {
            for (int i = 0; i < node.Children.Count; i++)
            {
                var current = node.Children[i];

                if (targetType.IsAssignableFrom(current.GetType()))
                {
                    if (matchCount == targetIndex)
                    {
                        node.Children.Insert(insertAfter ? i + 1 : i, newNode);
                        return true;
                    }

                    matchCount++;
                }

                if (current is CollectionNode nested)
                {
                    if (TryInsert(nested, targetType, targetIndex, newNode, insertAfter, ref matchCount))
                        return true;
                }
            }

            return false;
        }

        private static bool TryInsert(CollectionNode node, Type targetType, int targetIndex, AbilityNode newNode,
            bool insertAfter)
        {
            var matchCount = 0;
            return TryInsert(node, targetType, targetIndex, newNode, insertAfter, ref matchCount);
        }

        public static bool RemoveInstance(this CollectionNode sequence, AbilityNode node)
        {
            if (sequence == null || node == null)
            {
                Debug.LogWarning("RemoveInstance failed: null reference.");
                return false;
            }

            if (sequence.Children.Remove(node))
                return true;

            foreach (var child in sequence.Children.OfType<CollectionNode>())
            {
                if (child.RemoveInstance(node))
                    return true;
            }

            return false;
        }
    }
}
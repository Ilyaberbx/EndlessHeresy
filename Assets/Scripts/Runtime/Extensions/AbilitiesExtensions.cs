using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class AbilitiesExtensions
    {
        public static bool InsertAfter<TTarget>(this SequenceNode sequence, int occurrenceIndex, AbilityNode newNode)
            where TTarget : AbilityNode
        {
            if (sequence == null || newNode == null)
            {
                Debug.LogWarning("InsertAfter failed: null reference.");
                return false;
            }

            var matchCount = 0;

            for (var i = 0; i < sequence.Children.Count; i++)
            {
                if (sequence.Children[i] is TTarget)
                {
                    if (matchCount == occurrenceIndex)
                    {
                        sequence.Children.Insert(i + 1, newNode);
                        return true;
                    }

                    matchCount++;
                }
            }

            Debug.LogWarning(
                $"InsertAfter: No node of type {typeof(TTarget).Name} at index {occurrenceIndex} found in sequence.");
            return false;
        }

        public static bool InsertBefore<TTarget>(this SequenceNode sequence, int occurrenceIndex, AbilityNode newNode)
            where TTarget : AbilityNode
        {
            if (sequence == null || newNode == null)
            {
                Debug.LogWarning("InsertBefore failed: null reference.");
                return false;
            }

            var matchCount = 0;

            for (var i = 0; i < sequence.Children.Count; i++)
            {
                if (sequence.Children[i] is TTarget)
                {
                    if (matchCount == occurrenceIndex)
                    {
                        sequence.Children.Insert(i, newNode);
                        return true;
                    }

                    matchCount++;
                }
            }

            Debug.LogWarning(
                $"InsertBefore: No node of type {typeof(TTarget).Name} at index {occurrenceIndex} found in sequence.");
            return false;
        }
    }
}
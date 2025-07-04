using System.Linq;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class AnimatorsAggregatorComponent : MonoComponent
    {
        [SerializeField] private LayerAnimatorData[] _data;

        public void Play(string animationName, params AnimatorLayerType[] layers)
        {
            if (layers.IsNullOrEmpty())
            {
                return;
            }

            foreach (var layer in layers)
            {
                if (!TryGet(layer, out var animator))
                {
                    continue;
                }

                animator.Play(animationName, -1, 0f);
            }
        }

        public bool IsPlaying(string animationName, params AnimatorLayerType[] layers)
        {
            if (layers.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var layer in layers)
            {
                if (!TryGet(layer, out var animator))
                {
                    continue;
                }

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
                {
                    return false;
                }
            }

            return true;
        }

        public Animator GetAnimatorForLayer(AnimatorLayerType layer)
        {
            var data = _data.FirstOrDefault(temp => temp.LayerIdentifier == layer);
            return data?.Animator;
        }

        private bool TryGet(AnimatorLayerType identifier, out Animator animator)
        {
            var data = _data.FirstOrDefault(temp => temp.LayerIdentifier == identifier);

            if (data == null)
            {
                animator = null;
                return false;
            }

            animator = data.Animator;
            return true;
        }
    }
}
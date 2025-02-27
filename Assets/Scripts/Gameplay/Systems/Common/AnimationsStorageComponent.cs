using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using UnityEngine;
using Animation = EndlessHeresy.Gameplay.Animations.Animation;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class AnimationsStorageComponent : MonoComponent
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Animation[] _animations;

        public Animator Animator => _animator;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var animation in _animations)
            {
                animation.SetAnimator(_animator);
            }

            return Task.CompletedTask;
        }

        public bool TryGetAnimation<TAnimation>(out TAnimation animation) where TAnimation : Animation
        {
            var derivedAnimation = _animations.FirstOrDefault(temp => temp.GetType() == typeof(TAnimation));

            if (derivedAnimation == null)
            {
                animation = null;
                return false;
            }

            animation = derivedAnimation as TAnimation;
            return true;
        }
    }
}
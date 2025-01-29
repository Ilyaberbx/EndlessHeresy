using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement.Rotate
{
    public sealed class RotateAroundComponent : PocoComponent
    {
        private float _targetAngle;
        private readonly float _duration;

        public RotateAroundComponent(float duration) => _duration = duration;

        public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public async Task RotateAsync(Transform transform, float angle)
        {
            _targetAngle = angle;
            var startAngle = transform.eulerAngles.z;
            var elapsedTime = 0f;
            var duration = _duration;

            while (elapsedTime < duration)
            {
                var progress = elapsedTime / duration;
                var curveValue = rotationCurve.Evaluate(progress);

                var currentAngle = Mathf.Lerp(startAngle, _targetAngle, curveValue);
                transform.rotation = Quaternion.Euler(0, 0, currentAngle);

                elapsedTime += Time.deltaTime;
                await Task.Yield();
            }

            transform.rotation = Quaternion.Euler(0, 0, _targetAngle);
        }
    }
}
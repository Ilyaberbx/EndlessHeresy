using System.Threading.Tasks;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement.Rotate
{
    public sealed class RotateAroundComponent : PocoComponent
    {
        private float _targetAngle;

        public async Task RotateAsync(Transform transform, float angle, float duration, AnimationCurve curve)
        {
            _targetAngle = angle;
            var totalRotation = 0f;

            while (totalRotation < _targetAngle)
            {
                var step = duration * Time.deltaTime;
                var curveValue = curve.Evaluate(step);
                var stepAngle = Mathf.Rad2Deg * curveValue;

                // Prevent overshooting
                stepAngle = Mathf.Min(stepAngle, _targetAngle - totalRotation);

                transform.Rotate(Vector3.forward, stepAngle, Space.Self);
                totalRotation += stepAngle;

                await Task.Yield();
            }

            // Align final rotation to ensure accuracy
            var eulerAngles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, _targetAngle);
        }
    }
}
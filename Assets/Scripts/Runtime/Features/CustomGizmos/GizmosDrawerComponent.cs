using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Utilities;
using UnityEngine;

namespace EndlessHeresy.Runtime.CustomGizmos
{
    public class GizmosOverlapDrawer : MonoComponent
    {
        private CapsuleOverlapData _overlapData;
        private Vector2 _at;
        private float _gizmoDrawTime;
        private const float GizmoDuration = 1f;

        public void SetOverlapData(CapsuleOverlapData overlapData, Vector2 at)
        {
            _overlapData = overlapData;
            _at = at;
            _gizmoDrawTime = Time.time;
        }

        private void OnDrawGizmos()
        {
            if (Time.time - _gizmoDrawTime < GizmoDuration)
            {
                GizmosUtility.DrawWireCapsule2D(_overlapData, _at, Color.red);
            }
        }
    }
}
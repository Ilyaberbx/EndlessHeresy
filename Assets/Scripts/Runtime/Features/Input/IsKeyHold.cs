using UnityEngine;

namespace EndlessHeresy.Runtime.Input
{
    public sealed class IsKeyHold : KeyCondition
    {
        public IsKeyHold(KeyCode keyCode) : base(keyCode)
        {
        }

        public override bool Invoke() => InputService.GetKey(KeyCode);
    }
}
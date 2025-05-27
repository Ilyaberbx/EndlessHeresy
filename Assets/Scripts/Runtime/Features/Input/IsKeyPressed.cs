using UnityEngine;

namespace EndlessHeresy.Runtime.Input
{
    public sealed class IsKeyPressed : KeyCondition
    {
        public IsKeyPressed(KeyCode keyCode) : base(keyCode)
        {
        }

        public override bool Invoke() => InputService.GetKeyDown(KeyCode);
    }
}
using UnityEngine;

namespace EndlessHeresy.Gameplay.Conditions
{
    public sealed class IsKeyPressed : KeyCondition
    {
        public IsKeyPressed(KeyCode keyCode) : base(keyCode)
        {
        }

        public override bool Invoke() => InputService.GetKeyDown(KeyCode);
    }
}
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Operational
{
    public struct ShowFloatingMessageDto
    {
        public Vector2 At { get; }
        public string Message { get; }
        public float Duration { get; }
        public Color Color { get; }
        public Vector2 Direction { get; }

        public ShowFloatingMessageDto(Vector2 at, string message, float duration, Color color, Vector2 direction)
        {
            At = at;
            Message = message;
            Duration = duration;
            Color = color;
            Direction = direction;
        }
    }
}
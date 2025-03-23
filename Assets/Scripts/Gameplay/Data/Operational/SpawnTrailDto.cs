using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Operational
{
    public struct SpawnTrailDto
    {
        public Color Color { get; }
        public float LifeTime { get; }
        public string Name { get; }
        public Sprite Sprite { get; }
        public Vector2 At { get; }

        public SpawnTrailDto(float lifeTime, Color color, Vector2 at, Sprite sprite, string name)
        {
            LifeTime = lifeTime;
            Color = color;
            At = at;
            Sprite = sprite;
            Name = name;
        }
    }
}
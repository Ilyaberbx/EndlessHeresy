namespace EndlessHeresy.Gameplay.Data.Operational
{
    public struct ProcessAttackDto
    {
        public int Damage { get; }
        public float Force { get; }
        public float Radius { get; }
        public float DragForce { get; }

        public ProcessAttackDto(int damage, float force, float radius, float dragForce)
        {
            Damage = damage;
            Force = force;
            Radius = radius;
            DragForce = dragForce;
        }
    }
}
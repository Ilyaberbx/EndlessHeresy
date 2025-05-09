namespace EndlessHeresy.Gameplay.Animations
{
    public sealed class MovementAnimation : Animation
    {
        private static readonly int IsMoving = UnityEngine.Animator.StringToHash("IsMoving");

        public void SetMoving(bool isMoving) => Animator.SetBool(IsMoving, isMoving);
    }
}
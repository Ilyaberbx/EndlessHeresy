namespace EndlessHeresy.Runtime.Animations
{
    public sealed class MovementAnimation : BaseAnimation
    {
        private static readonly int IsMoving = UnityEngine.Animator.StringToHash("IsMoving");

        public void SetMoving(bool isMoving) => Animator.SetBool(IsMoving, isMoving);
    }
}
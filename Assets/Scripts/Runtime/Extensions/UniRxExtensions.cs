using UniRx;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class UniRxExtensions
    {
        public static void SetDirty<T>(this ReactiveProperty<T> property)
        {
            property.SetValueAndForceNotify(property.Value);
        }
    }
}
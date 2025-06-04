using System;
using System.Linq;
using UniRx;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class UniRxExtensions
    {
        public static IObservable<CollectionAddEvent<T>> ObserveAddWithInitial<T>(
            this IReadOnlyReactiveCollection<T> collection)
        {
            return collection.ObserveAdd()
                .StartWith(collection.Select((item, index) => new CollectionAddEvent<T>(index, item)));
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using VContainer;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class VContainerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IObjectResolver resolver, Lifetime lifetime = Lifetime.Singleton)
        {
            var registrationBuilder = new RegistrationBuilder(typeof(T), lifetime);
            var registration = registrationBuilder.Build();
            return (T)resolver.Resolve(registration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IObjectResolver resolver, Lifetime lifetime = Lifetime.Singleton,
            params object[] args)
        {
            var registrationBuilder = new RegistrationBuilder(typeof(T), lifetime);

            if (args is { Length: > 0 })
            {
                foreach (var obj in args)
                {
                    registrationBuilder.WithParameter(obj.GetType(), obj);
                }
            }

            var registration = registrationBuilder.Build();
            return (T)resolver.Resolve(registration);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Instantiate(this IObjectResolver resolver, Type forType,
            Lifetime lifetime = Lifetime.Singleton,
            params object[] args)
        {
            var registrationBuilder = new RegistrationBuilder(forType, lifetime);

            if (args is { Length: > 0 })
            {
                foreach (var obj in args)
                {
                    registrationBuilder.WithParameter(obj.GetType(), obj);
                }
            }

            var registration = registrationBuilder.Build();
            return resolver.Resolve(registration);
        }
    }
}
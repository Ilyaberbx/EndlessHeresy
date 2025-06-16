using System;
using System.Collections.Generic;

namespace EndlessHeresy.Runtime.Animations
{
    public sealed class AnimationsEventListener : MonoComponent
    {
        private readonly Dictionary<string, List<Action>> _listeners = new();

        public void Register(string eventName, Action callback)
        {
            if (!_listeners.TryGetValue(eventName, out var list))
            {
                list = new List<Action>();
                _listeners[eventName] = list;
            }

            if (!list.Contains(callback))
                list.Add(callback);
        }

        public void Unregister(string eventName, Action callback)
        {
            if (!_listeners.TryGetValue(eventName, out var list))
            {
                return;
            }

            list.Remove(callback);

            if (list.Count == 0)
            {
                _listeners.Remove(eventName);
            }
        }

        public void OnAnimationEvent(string eventName)
        {
            if (!_listeners.TryGetValue(eventName, out var list))
            {
                return;
            }

            foreach (var callback in list.ToArray())
            {
                callback?.Invoke();
            }
        }
    }
}
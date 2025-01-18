using System;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Common
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class TriggerObserverComponent<TComponent> : MonoComponent where TComponent : MonoComponent
    {
        public event Action<TComponent> OnTriggerEnter;
        public event Action<TComponent> OnTriggerExit;
        public event Action<TComponent> OnTriggerStay;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out TComponent component))
            {
                OnTriggerEnter?.Invoke(component);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out TComponent component))
            {
                OnTriggerExit?.Invoke(component);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out TComponent component))
            {
                OnTriggerStay?.Invoke(component);
            }
        }
    }
}
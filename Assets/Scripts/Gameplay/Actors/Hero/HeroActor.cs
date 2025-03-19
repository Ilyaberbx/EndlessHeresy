using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Health;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IStateMachineContext
    {
        private HealthComponent _healthComponent;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _healthComponent = GetComponent<HealthComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(1);
            }
        }
    }
}
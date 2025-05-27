using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.Services.Gameplay.Factory;
using EndlessHeresy.Runtime.Tags;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Actors
{
    public sealed class ItemPickUpActor : MonoActor
    {
        private const float DisappearTime = 0.2f;

        [SerializeField] private ItemType _itemType;
        [SerializeField] private Ease _disappearEase;
        [SerializeField] private Collider2D _collider;
        
        private IGameplayFactoryService _gameplayFactoryService;

        private HeroTriggerObserver _heroObserver;

        [Inject]
        public void Construct(IGameplayFactoryService gameplayFactoryService)
        {
            _gameplayFactoryService = gameplayFactoryService;
        }

        protected override  Task OnInitializeAsync()
        {
            _heroObserver = GetComponent<HeroTriggerObserver>();
            _heroObserver.OnTriggerEnter += OnTriggerEntered;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _heroObserver.OnTriggerEnter -= OnTriggerEntered;
        }

        private void OnTriggerEntered(HeroTagComponent tagComponent)
        {
            if (!tagComponent.Owner.TryGetComponent(out InventoryComponent inventory))
            {
                return;
            }

            inventory.Add(_itemType);

            _collider.enabled = false;
            DisappearAsync().Forget();
        }

        private async Task DisappearAsync()
        {
            await Transform
                .DOScale(Vector3.zero, DisappearTime)
                .SetEase(_disappearEase)
                .SetId(this)
                .AsTask(DestroyCancellationToken);

            _gameplayFactoryService.Dispose(this);
        }
    }
}
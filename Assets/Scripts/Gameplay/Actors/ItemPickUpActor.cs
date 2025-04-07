using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Inventory;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Tags;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Actors
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

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _heroObserver = GetComponent<HeroTriggerObserver>();
            _heroObserver.OnTriggerEnter += OnTriggerEntered;
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
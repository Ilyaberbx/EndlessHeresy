using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using EndlessHeresy.Gameplay.Data.Static;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay.Services.FloatingMessages.Factory
{
    public sealed class FloatingMessagesFactory : IFloatingMessagesFactory, IInitializable
    {
        private readonly Transform _parent;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private FloatingMessagesConfiguration _configuration;

        public FloatingMessagesFactory(Transform parent, IGameplayStaticDataService gameplayStaticDataService)
        {
            _parent = parent;
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        public void Initialize()
        {
            _configuration = _gameplayStaticDataService.FloatingMessagesConfiguration;
        }

        public FloatingMessageView Create()
        {
            var prefab = _configuration.Prefab;
            return Object.Instantiate(prefab, _parent, true);
        }

        public void Dispose(FloatingMessageView messageView)
        {
            if (messageView.IsNullOrDestroyed())
            {
                return;
            }

            DOTween.Kill(messageView);
            messageView.Destroy();
        }
    }
}
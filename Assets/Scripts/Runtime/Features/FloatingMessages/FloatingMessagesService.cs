using System;
using System.Threading.Tasks;
using EndlessHeresy.Meta.UI.ViewComponents;
using EndlessHeresy.Runtime.Camera;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.FloatingMessages.Factory;
using EndlessHeresy.Runtime.Scopes.Gameplay.Services.StaticData;
using UnityEngine.Pool;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.FloatingMessages
{
    public sealed class  FloatingMessagesService : IFloatingMessagesService, IPostInitializable, IDisposable
    {
        private readonly IFloatingMessagesFactory _factory;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private readonly ICameraService _cameraService;
        private IObjectPool<FloatingMessageView> _messagePool;

        public FloatingMessagesService(IFloatingMessagesFactory factory,
            IGameplayStaticDataService gameplayStaticDataService,
            ICameraService cameraService)
        {
            _factory = factory;
            _gameplayStaticDataService = gameplayStaticDataService;
            _cameraService = cameraService;
        }

        public void PostInitialize()
        {
            var configuration = _gameplayStaticDataService.FloatingMessagesConfiguration;

            _messagePool = new ObjectPool<FloatingMessageView>(
                CreateMessage,
                OnMessageGet,
                OnMessageRelease,
                OnMessageDestroy,
                collectionCheck: false,
                defaultCapacity: configuration.PoolData.DefaultCapacity,
                maxSize: configuration.PoolData.MaxSize
            );
        }

        public void Dispose() => HideAll();

        public async Task ShowAsync(ShowFloatingMessageQuery messageQuery)
        {
            var messageInstance = _messagePool.Get();
            messageInstance.transform.position = messageQuery.At;
            messageInstance.SetMessage(messageQuery.Message);
            messageInstance.SetColor(messageQuery.Color);
            messageInstance.SetCamera(_cameraService.MainCamera);
            await messageInstance.ShowAsync(messageQuery.Duration, messageQuery.Direction);
            _messagePool.Release(messageInstance);
        }

        private void HideAll() => _messagePool.Clear();
        private FloatingMessageView CreateMessage() => _factory.Create();

        private void OnMessageGet(FloatingMessageView message)
        {
            if (message == null)
            {
                return;
            }

            message.gameObject.SetActive(true);
        }

        private void OnMessageDestroy(FloatingMessageView message) => _factory.Dispose(message);

        private void OnMessageRelease(FloatingMessageView message)
        {
            message.gameObject.SetActive(false);
            message.OnPoolRelease();
        }
    }
}
using System;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Data.Operational;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.Services.FloatingMessages.Factory;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine.Pool;
using VContainer.Unity;

namespace EndlessHeresy.UI.Services.FloatingMessages
{
    public sealed class FloatingMessagesService : IFloatingMessagesService, IPostInitializable, IDisposable
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

        public async Task ShowAsync(ShowFloatingMessageDto messageDto)
        {
            var messageInstance = _messagePool.Get();
            messageInstance.transform.position = messageDto.At;
            messageInstance.SetMessage(messageDto.Message);
            messageInstance.SetColor(messageDto.Color);
            messageInstance.SetCamera(_cameraService.MainCamera);
            await messageInstance.ShowAsync(messageDto.Duration, messageDto.Direction);
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
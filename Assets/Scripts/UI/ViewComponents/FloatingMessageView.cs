using System.Threading.Tasks;
using Better.Commons.Runtime.Components.UI;
using DG.Tweening;
using EndlessHeresy.Extensions;
using TMPro;
using UnityEngine;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class FloatingMessageView : UIMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Canvas _selfCanvas;

        public void SetMessage(string message) => _messageText.text = message;
        public void SetColor(Color color) => _messageText.color = color;
        public void SetCamera(Camera camera) => _selfCanvas.worldCamera = camera;

        public Task ShowAsync(float duration, Vector2 direction)
        {
            var endValue = transform.position.ToVector2() + direction;

            return transform.DOMove(endValue, duration)
                .AsTask(destroyCancellationToken);
        }

        public void OnPoolRelease()
        {
            _messageText.text = string.Empty;
            _messageText.color = Color.white;
        }
    }
}
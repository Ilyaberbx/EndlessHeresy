using Better.Commons.Runtime.Components.UI;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class ProgressBarView : UIMonoBehaviour
    {
        private const float MinValue = 0f;
        private const float MaxValue = 1f;

        [SerializeField] private Image _progressImage;

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp(progress, MinValue, MaxValue);
            _progressImage.fillAmount = progress;
        }

        public void SetActive(bool active) => _progressImage.gameObject.SetActive(active);
    }
}
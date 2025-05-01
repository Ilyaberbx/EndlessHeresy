using Better.Commons.Runtime.Components.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class StatusEffectItemView : UIMonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private ProgressBarView _progressBarView;
        [SerializeField] private GameObject _stackContainer;
        [SerializeField] private GameObject _temporaryContainer;
        [SerializeField] private TextMeshProUGUI _stackText;

        public void SetIcon(Sprite icon) => _iconImage.sprite = icon;
        public void SetProgress(float progress) => _progressBarView.SetProgress(progress);
        public void SetStackCount(int stackCount) => _stackText.text = stackCount.ToString();
        public void SetTemporary(bool temporary) => _temporaryContainer.gameObject.SetActive(temporary);
        public void SetStackable(bool stackable) => _stackContainer.gameObject.SetActive(stackable);
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}
using System.Globalization;
using Better.Commons.Runtime.Components.UI;
using EndlessHeresy.Gameplay.Abilities.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class AbilityItemView : UIMonoBehaviour
    {
        private const string CooldownFormat = "F2";
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _cooldownText;
        [SerializeField] private Image _cooldownTimerImage;
        [SerializeField] private GameObject _readyContainer;
        [SerializeField] private GameObject _inCooldownContainer;
        [SerializeField] private GameObject _inUseContainer;

        public void SetActive(bool active) => gameObject.SetActive(active);
        public void SetIcon(Sprite icon) => _iconImage.sprite = icon;

        public void SetCooldown(float cooldown, float maxCooldown)
        {
            var rawProgress = cooldown / maxCooldown;
            var progress = Mathf.Clamp(rawProgress, 0f, maxCooldown);
            _cooldownTimerImage.fillAmount = progress;
            _cooldownText.text = cooldown.ToString(CooldownFormat, CultureInfo.InvariantCulture);
        }

        public void SetState(AbilityState state)
        {
            _readyContainer.gameObject.SetActive(state == AbilityState.Ready);
            _inCooldownContainer.gameObject.SetActive(state == AbilityState.Cooldown);
            _inUseContainer.gameObject.SetActive(state == AbilityState.InUse);
        }
    }
}
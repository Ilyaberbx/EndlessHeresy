using Better.Commons.Runtime.Components.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.ViewComponents
{
    public sealed class AttributeItemView : UIMonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;

        public void SetIcon(Sprite icon) => _iconImage.sprite = icon;
        public void SetName(string name) => _nameText.text = name;
        public void SetValue(int value) => _valueText.text = value.ToString();
    }
}
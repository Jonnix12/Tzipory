using Shamans;
using Systems.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayeLogic.UIElements
{
    public class ShamanUiHandler : BaseUIElement
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _splash;
        private Shaman _shaman;
        
        public void Init(Shaman shaman)
        {
            _shaman = shaman;
            _splash.sprite = _shaman.SpriteRenderer.sprite;
            Show();
            UpdateUIData();
        }

        public override void Show()
        {
            _shaman.HP.OnCurrentValueChanged += UpdateUIData;
            base.Show();
        }

        public override void Hide()
        {
            _shaman.HP.OnCurrentValueChanged -= UpdateUIData;
            base.Hide();
        }

        private void UpdateUIData()
        {
            _healthBar.value  = _shaman.HP.CurrentValue / _shaman.HP.BaseValue;
            _fill.color = Color.Lerp(Color.green,Color.red,_shaman.HP.CurrentValue/_shaman.HP.BaseValue);
        }
    }
}
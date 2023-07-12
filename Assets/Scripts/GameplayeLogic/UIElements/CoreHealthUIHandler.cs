using System;
using Systems.UISystem;
using TMPro;
using UnityEngine;

namespace GameplayeLogic.UIElements
{
    public class CoreHealthUIHandler : BaseUIElement
    {
        [SerializeField] private TMP_Text _coreHP;
        [SerializeField] private TMP_Text _coreMaxHP;
        
        private CoreTemple _coreTemple;

        private void Start()
        {
            Show();//temp
        }

        public override void Show()
        {
            _coreTemple = GameManager.CoreTemplete;
            _coreTemple.HP.OnCurrentValueChanged += UpdateUiData;
            UpdateUiData();
            base.Show();
        }

        public override void Hide()
        {
            _coreTemple.HP.OnCurrentValueChanged -= UpdateUiData;
            base.Hide();
        }
        
        private void UpdateUiData()
        {
            _coreHP.text = _coreTemple.HP.CurrentValue.ToString();
            _coreMaxHP.text = $"/{_coreTemple.HP.BaseValue}";
        }
    }
}
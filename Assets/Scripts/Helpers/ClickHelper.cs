using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tzipory.Helpers
{
    public class ClickHelper : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
    {
        public event Action OnClick;
        public event Action OnEnterHover;
        public event Action OnExitHover;

        public bool IsHover { get; private set; }   
        
        private const int CLICKABLE_LAYER_INDEX = 11;

        private void OnValidate()
        {
            gameObject.layer = CLICKABLE_LAYER_INDEX;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
            Debug.Log("OnClick");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHover = true;
            OnEnterHover?.Invoke();
            Debug.Log("On enter hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHover = false;
            OnExitHover?.Invoke();
            Debug.Log("On exit hover");
        }
    }
   
}
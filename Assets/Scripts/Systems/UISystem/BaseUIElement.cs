using System;
using UnityEngine;

namespace Systems.UISystem
{
    public abstract class BaseUIElement : MonoBehaviour , IUIElement
    {
        public string ElementName => gameObject.name;
        public Action OnShow { get; }

        public Action OnHide { get; }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }

    public interface IUIElement
    {
        public string ElementName { get; }
        public Action OnShow { get; }
        public Action OnHide { get; }
        
        void Show();
        void Hide();
    }
}
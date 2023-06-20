using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMP_HP_Bar : MonoBehaviour
{
    [SerializeField]
    Image fillImage;
    [SerializeField]
    Transform fillSprite;
    [SerializeField]
    float drainDuration;

    float _maxValue;
    //float targetValue;
    Coroutine _runningSmoothBar;
  
    public void Init(float max)
    {
        fillImage.fillAmount = 1f; //just to make sure. later on this will be removed and it will just read the value
        _maxValue = max;
    }

    public void SetBarValue(float value)
    {
        fillImage.fillAmount = value/ _maxValue;
    }

    public void SetBarValueSmoothly(float value)
    {
        //targetValue = value;
        if(_runningSmoothBar != null)
        {
            StopCoroutine(_runningSmoothBar);
        }
        _runningSmoothBar = StartCoroutine(SmoothBar(drainDuration, value / _maxValue));
    }

    IEnumerator SmoothBar(float duration, float targetValue)
    {
        float t = 0f;
        float _startValue = fillImage.fillAmount;
        float _currentValue = fillImage.fillAmount;
        while(t<=duration)
        {
            _currentValue = Mathf.Lerp(_startValue, targetValue, t);
            t += Time.deltaTime / duration;
            SetBarValue(_currentValue);
            yield return null;
        }
    }


}

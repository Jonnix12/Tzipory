using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProximityIndicatorHandler
{
    public static System.Action TEMP_CallAll_TAB;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _currentSprite;
    [SerializeField] private ProximityConfig _proximityConfig;
    [SerializeField] Tzipory.Helpers.ClickHelper clickHelper;
    
    private float _range;

    private bool _isToggleOn;
    private bool _isLock;

    
    public void Init(float range)
    {
        _range = range;
        _isLock = false;
        _isToggleOn = false;

        _spriteRenderer.transform.localScale = new Vector3(_range, _range,1);

        foreach (var condition in _proximityConfig.IndicatorConditions)
        {
            switch (condition)
            {
                case IndicatorCondition.AnyShamanSelected:
                    //Subscribe to OnAnyShamanSelected
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanSelected += () => SetToActiveAndLock(true, true);
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanDeselected += () => SetToActiveAndLock(false, false);
                    break;
                case IndicatorCondition.HoverSelf:
                    //_spriteRenderer.
                    clickHelper.OnEnterHover += () => SetToActive(true);
                    clickHelper.OnExitHover += () => SetToActive(false);
                    break;
                case IndicatorCondition.AllCall:
                    //Subscribe to AllCall
                    TEMP_CallAll_TAB += ToggleActive;
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// MUST CALL THIS TO UNSUB FROM EVENTS!
    /// </summary>
    public void Disable()
    {
        foreach (var condition in _proximityConfig.IndicatorConditions)
        {
            switch (condition)
            {
                case IndicatorCondition.AnyShamanSelected:
                    //Subscribe to OnAnyShamanSelected
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanSelected -= () => SetToActiveAndLock(true, true);
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanDeselected -= () => SetToActiveAndLock(false, false);
                    break;
                case IndicatorCondition.HoverSelf:
                    clickHelper.OnEnterHover -= () => SetToActive(true);
                    clickHelper.OnExitHover -= () => SetToActive(false);
                    //
                    break;
                case IndicatorCondition.AllCall:
                    //Subscribe to AllCall
                    TEMP_CallAll_TAB -= ToggleActive;
                    break;
                default:
                    break;
            }
        }
    }


    void SetToActive(bool doActive)
    {
        //Some logic and stuff
        if (_isLock)
            return;
        _spriteRenderer.enabled = doActive;
    }
    void ToggleActive()
    {
        //Some logic and stuff
        if (_isLock)
            return;

        _isToggleOn = !_isToggleOn;
        _spriteRenderer.enabled = _isToggleOn;
    }

    void SetToActiveAndLock(bool doActive, bool doLock)
    {
        //Some logic and stuff
        _isLock = doLock;
        _spriteRenderer.enabled = doActive;
    }
}

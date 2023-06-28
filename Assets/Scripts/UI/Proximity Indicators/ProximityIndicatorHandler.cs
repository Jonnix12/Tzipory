using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProximityIndicatorHandler
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _currentSprite;
    [SerializeField] private ProximityConfig _proximityConfig;
    private float _range;

    public void Init()
    {
        foreach (var condition in _proximityConfig.IndicatorConditions)
        {
            switch (condition)
            {
                case IndicatorCondition.AnyShamanSelected:
                    //Subscribe to OnAnyShamanSelected
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanSelected += () => SetToActive(true);
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanDeselected += () => SetToActive(false);
                    break;
                case IndicatorCondition.HoverSelf:
                    //
                    break;
                case IndicatorCondition.AllCall:
                    //Subscribe to AllCall
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
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanSelected -= () => SetToActive(true);
                    MovementSystem.HerosMovementSystem.TempHeroMovementManager.OnAnyShamanDeselected -= () => SetToActive(false);
                    break;
                case IndicatorCondition.HoverSelf:
                    //
                    break;
                case IndicatorCondition.AllCall:
                    //Subscribe to AllCall
                    break;
                default:
                    break;
            }
        }
    }


    void SetToActive(bool doActive)
    {
        //Some logic and stuff
        _spriteRenderer.enabled = doActive;
    }
}

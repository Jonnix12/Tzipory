using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProximityIndicatorHandler
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _currentSprite;
    [SerializeField] private float _range;
    [SerializeField] private ProximityConfig _proximityConfig;

    public void Init()
    {
        foreach (var condition in _proximityConfig.IndicatorConditions)
        {
            switch (condition)
            {
                case IndicatorCondition.AnyShamanSelected:
                    //Subscribe to OnAnyShamanSelected
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
}

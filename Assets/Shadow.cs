using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shadowRenderer;
    [SerializeField] private SpriteRenderer _proximityRenderer;
    [SerializeField] private SpriteMask _mask;


    public bool IsOn;
    //private void Awake()
    //{
    //    IsOn = false;
    //}

    public void SetShadow(Sprite shadowSprite, float range)
    {
        gameObject.SetActive(true);
        IsOn = true;
        
        _shadowRenderer.sprite = shadowSprite;
        _mask.sprite = shadowSprite;
        _shadowRenderer.gameObject.SetActive(true);

        _proximityRenderer.transform.localScale = new Vector3(range, range, 1);

    }

    public void ClearShadow()
    {
        IsOn = false;
        gameObject.SetActive(false);
    }
}

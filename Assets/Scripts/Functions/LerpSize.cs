using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LerpSize : MonoBehaviour
{
    public float lerpSizeIncrease = .5f;
    [Range(0.0f, 1.0f)]
    public float colourVariance = 0.5f;

    public GameObject indicator;

    Vector3 originalSize;
    Vector3 scale;

    SpriteRenderer spriteRenderer;
    Color color = Color.white;
    Color originalColor;
    Color darkerColor;

    bool clicked;

    public float scalingSizeTimer;

    /// <summary>
    /// Grab sprite component for particular game object.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Set up the clicking colours, sizes to lerp to, including original size and the lerped size.
    /// </summary>
    private void Start()
    {
        originalColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
        darkerColor = new Color((float)(spriteRenderer.color.r - colourVariance), (float)(spriteRenderer.color.g - colourVariance), (float)(spriteRenderer.color.b - colourVariance));

        originalSize = new Vector3(indicator.transform.localScale.x, indicator.transform.localScale.y, indicator.transform.localScale.x);
        scale = new Vector3((indicator.transform.localScale.x + lerpSizeIncrease), (indicator.transform.localScale.y + lerpSizeIncrease), indicator.transform.localScale.z);
    }

    /// <summary>
    /// Change object to darker colour.
    /// </summary>
    private void ChangeColor()
    {
        spriteRenderer.color = darkerColor;
        clicked = true;
    }

    /// <summary>
    /// Return object to original colour.
    /// </summary>
    private void RevertColor()
    {
        spriteRenderer.color = originalColor;
        clicked = false;

    }

    /// <summary>
    /// On the mouse entering the trigger area, execute lerping size.
    /// </summary>
    private void OnMouseEnter()
    {
        iTween.ScaleTo(indicator, scale, scalingSizeTimer);
    }

    /// <summary>
    /// If mouse is over trigger area, handle colour changing.
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!clicked)
            {
                ChangeColor();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (clicked)
            {
                RevertColor();
            }
        }
    }

    /// <summary>
    /// On the mouse exiting the trigger area, return to original size and original colour.
    /// </summary>
    private void OnMouseExit()
    {
        iTween.ScaleTo(indicator, originalSize, 2f);
        RevertColor();
    }
}
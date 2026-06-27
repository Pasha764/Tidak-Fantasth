using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarRage : MonoBehaviour
{
    public int maxRage = 100;
    public Image rageImage;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rageImage.fillAmount = maxRage / 0f;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    private Image barImage;
    void Start()
    {
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();
        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
    }

    public void Damage(float damage)
    {
        if (Health.totalHealth >= 0.15f)
        {
            Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 0f;
        }
        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}

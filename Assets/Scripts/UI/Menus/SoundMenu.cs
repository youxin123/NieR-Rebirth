﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundMenu : Menu
{
    [SerializeField] private SliderButton bgmSlider;
    [SerializeField] private SliderButton efxSlider;
    
    protected override void Update()
    {
        if (!IsCurrentActive()) return;

        base.Update();

        if (STBInput.GetButtonDown("Cancel"))
        {
            SoundManager.instance.PlayUiEfx(UiEfx.CANCEL);
            if (LastPressed == null)
            {
                MenuController.instance.BackToOptionsMenu();
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                LastPressed.SetPressed(false);
                EventSystem.current.SetSelectedGameObject(LastFocused.gameObject);
            }
        }
    }

    protected override void OnFadeIn(float duration)
    {
        bgmSlider.value = GameManager.bgmVolume * 10f;
        efxSlider.value = GameManager.efxVolume * 10f;

        foreach (StaticImage staticImage in staticImages)
        {
            staticImage.FadeIn(duration * 0.6f);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].FadeIn(duration * 0.6f, duration * 0.2f * i);
        }
    }

    //Sound Options Menu Controls

    public void OnBgmValueChanged()
    {
        GameManager.SetBgmVolume(bgmSlider.value / 10f);
    }

    public void OnEfxValueChanged()
    {
        GameManager.SetEfxVolume(efxSlider.value / 10f);
    }
}
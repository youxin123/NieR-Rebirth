﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InPlayOptionsMenu : Menu
{
    [SerializeField] private InfoBox infoBox;

    protected override void Update()
    {
        if (!IsCurrentActive()) return;

        base.Update();

        if (STBInput.GetButtonDown("Cancel"))
        {
            SoundManager.instance.PlayUiEfx(UiEfx.CANCEL);
            if (LastPressed == null)
            {
                FadeOut(0.4f);
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                LastPressed.SetPressed(false);
            }
        }
    }

    protected override void OnFadeIn(float duration)
    {
        infoBox.FadeIn(0.6f * duration);
        foreach (StaticImage staticImage in staticImages)
        {
            staticImage.FadeIn(duration * 0.6f);
        }
        foreach (TitleText titleText in titleTexts)
        {
            titleText.FadeIn(duration * 0.4f);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].FadeIn(duration * 0.6f, duration * (0.3f + 0.1f * i));
        }
    }

    protected override void OnFadeOut(float duration)
    {
        base.OnFadeOut(duration);
        infoBox.FadeOut(0.6f * duration);
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        PauseMenu.instance.BackToPauseMenu();
    }
}
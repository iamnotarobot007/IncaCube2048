using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BaseScreen[] screen;
    public BaseScreen CurrentScreen;
    public static UIManager instance;
    
    private void Start()
    {
        instance = this;
        CurrentScreen.canvas.enabled = true;
    }
    public void SwitchScreen(ScreenType screenType)
    {
        CurrentScreen.canvas.enabled = false;
        foreach (BaseScreen baseScreen in screen)
        {
            if (baseScreen.screenType == screenType)
            {
                baseScreen.canvas.enabled = true;
                CurrentScreen = baseScreen;
                break;
            }

        }
    }
}
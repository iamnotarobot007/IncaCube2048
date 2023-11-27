using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : BaseScreen
{
    public Canvas _settingsPopup;

    public void MainMenu()
    {
        UIManager.instance.SwitchScreen(ScreenType.HomeScreen);
        SoundManager.inst.PlaySound(SoundName.ButtonClick);
    }

    public void OpenSettings()
    {
        _settingsPopup.enabled = true;
        SoundManager.inst.PlaySound(SoundName.ButtonClick);
    }

    public void CloseSettings()
    {
        _settingsPopup.enabled = false;
        SoundManager.inst.PlaySound(SoundName.BackButtonClick);
    }

}

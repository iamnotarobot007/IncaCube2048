using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : BaseScreen
{
    public Canvas _settingsPopup;
    public Canvas _ShopPopup;
    public int _TurnDuration;
    public int _rotateCam = 45;

    public void StartGame()
    {
        UIManager.instance.SwitchScreen(ScreenType.Gameplay);
        SoundManager.inst.PlaySound(SoundName.ButtonClick);
        CameraController.instance.TurnCamera(Quaternion.Euler(_rotateCam, 0, 0), _TurnDuration);
    }

    public void OpenShop()
    {
      
        _ShopPopup.enabled = true;
        SoundManager.inst.PlaySound(SoundName.ButtonClick);
    }

    public void CloseShop()
    {
        _ShopPopup.enabled = false;
        SoundManager.inst.PlaySound(SoundName.BackButtonClick);
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

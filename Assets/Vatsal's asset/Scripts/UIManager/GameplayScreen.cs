using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : BaseScreen
{
    public Canvas _settingsPopup;
    public Canvas _ShopPopup;
    public int _TurnDuration;

    [SerializeField] Player player;
    public void StopGame()
    {
        SoundManager.inst.PlaySound(SoundName.GameOver);
        UIManager.instance.SwitchScreen(ScreenType.Gameover);
        CameraController.instance.TurnCamera(Quaternion.Euler(0, 0, 0), _TurnDuration);
       
    }

    public void OpenBombShop()
    {
        if (player.Bombcount == 0)
            _ShopPopup.enabled = true;
        SoundManager.inst.PlaySound(SoundName.ButtonClick);
    }

    public void OpenRainBowShop()
    {
        if (player.RainBowcount == 0)
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
    public void Restart()
    {
        CubeSpawner.Instance.Resetcubes();
        _settingsPopup.enabled = false;
        Score.inst.ResetScore();
        SoundManager.inst.PlaySound(SoundName.ButtonClick);

    }

    public void HomeBtn()
    {
        CubeSpawner.Instance.Resetcubes();
        _settingsPopup.enabled = false;
        UIManager.instance.SwitchScreen(ScreenType.HomeScreen);
        Score.inst.ResetScore();
        CameraController.instance.TurnCamera(Quaternion.Euler(0, 0, 0), _TurnDuration);
        SoundManager.inst.PlaySound(SoundName.ButtonClick);

    }
}

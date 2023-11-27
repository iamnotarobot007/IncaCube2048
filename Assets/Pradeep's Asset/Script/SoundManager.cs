using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource bg;
    public Sound[] clips;

    public  RawImage[] soundBTNS;
    public  RawImage[] MusicBTNS;

    public Texture2D SoundON;
    public Texture2D SoundOFF;
    public Texture2D MusicON;
    public Texture2D MusicOFF;


    private bool isBgMuted = false;
    private bool isAudioSourceMuted = false;

    private void Awake()
    {
        inst = this;
    }
    public void PlaySound(SoundName name)
    {
        foreach (var item in clips)
        {
            if (item.name == name)
            {
                audioSource.PlayOneShot(item.clip);
                break;
            }
        }
    }
    public void ToggleMusic()
    {
        bg.mute = isBgMuted = !isBgMuted;
        SoundManager.inst.PlaySound(SoundName.SoundButtonClick);

        foreach (RawImage Images in MusicBTNS)
        {
            Images.texture = (isBgMuted) ?  MusicOFF :MusicON ;
        }

    }

    public void ToggleSound()
    {
        isAudioSourceMuted = !isAudioSourceMuted;
        audioSource.mute = isAudioSourceMuted;
        SoundManager.inst.PlaySound(SoundName.SoundButtonClick);

        foreach (RawImage Images in soundBTNS)
        {
            if (isAudioSourceMuted == true)
                Images.texture = SoundOFF;
            else
                Images.texture = SoundON;
        } 
    }
}

[System.Serializable]
public class Sound
{
    public SoundName name;
    public AudioClip clip;
}
public enum SoundName
{
    CubeCollision, ButtonClick, GameOver, BackButtonClick,ShopClick,SoundButtonClick,Bomb,RainBow
}

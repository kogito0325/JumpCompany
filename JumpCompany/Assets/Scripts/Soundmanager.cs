using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soundmanager : MonoBehaviour
{
    public AudioSource BGMsource;

    public AudioSource SEsource;

    private void Start()
    {
        BGMsource.volume = DataManager.instance.bgmVolume;
        SEsource.volume = DataManager.instance.soundVolume;
    }

    public void SetBGMVolume(float volume)
    {
        DataManager.instance.bgmVolume = volume;
        BGMsource.volume = volume;
    }

    public void SetSEvolume(float volume)
    {
        DataManager.instance.soundVolume = volume;
        SEsource.volume = volume;
    }

    public void OnSEAudio()
    {
        SEsource.Play();
    }

    public void BGMMute()
    {
        BGMsource.volume = 0;

    }

    public void SEMute()
    {
        SEsource.volume = 0;
    }
}

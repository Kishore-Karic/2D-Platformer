using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public Slider musicVolumeSlider, sfxVolumeSlider;
    public AudioMixer musicMixer, sfxMixer;
    private float musicValue, sfxValue;

    private void Start()
    {
        musicMixer.GetFloat("MusicVolume", out musicValue);
        musicVolumeSlider.value = musicValue;
        sfxMixer.GetFloat("SfxVolume", out sfxValue);
        sfxVolumeSlider.value = sfxValue;
    }

    public void SetMusicVolume()
    {
        musicMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetSfxVolume()
    {
        sfxMixer.SetFloat("SfxVolume", sfxVolumeSlider.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer master;
    public AudioMixer sound;
    public Slider masterslider;
    public Slider soundslider;
    public TMP_Dropdown dropdown;
    Resolution[] resolutions;
    private void Start()
    {   
        resolutions =  Screen.resolutions;
        if(dropdown!= null)
        {
            dropdown.ClearOptions();
        }
        List<string> options = new List<string>();
        int curr = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                curr = i;
            }

        }
        if (dropdown != null)
        {
            dropdown.AddOptions(options);
            dropdown.value = curr;
            dropdown.RefreshShownValue();
        }
        
        float volume;
        master.GetFloat("masterVolume", out volume);
        masterslider.value = volume;
        sound.GetFloat("soundVolume", out volume);
        soundslider.value = volume;


    }
    public void SetMasterVolume(float volume)
    {
        master.SetFloat("masterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        sound.SetFloat("soundVolume", volume);
    }

        public void SetFullscreen(bool isfull)
    {
        Screen.fullScreen = isfull;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

}

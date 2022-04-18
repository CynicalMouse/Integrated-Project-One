using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    //reference to audio mixer
    public AudioMixer audioMixer;

    //refernece to drop down
    public TMPro.TMP_Dropdown resolutionDropDown;

    //store resolutions
    Resolution[] resolutions;


    private void Start()
    {
        resolutions = Screen.resolutions;

        // clear out options in drop down
        resolutionDropDown.ClearOptions();

        // create list to store options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // loop for number of resolutions
        for (int i = 0; i <resolutions.Length; i++)
        {
            // convert resolution to string to display
            string option = resolutions[i].width + " x " + resolutions[i].height;
            // add it to array of options
            options.Add(option);
            
            // compare current resolution with actual screen resolution
            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                // if they match store it
                currentResolutionIndex = i;
            }

        }

        //add array of options to drop down
        resolutionDropDown.AddOptions(options);

        //set game resolution to monitor resolution
        resolutionDropDown.value = currentResolutionIndex;
        // show it
        resolutionDropDown.RefreshShownValue();
    }

    public void setResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        // apply volume from slider (Mathf.Log10(volume)*20 keeps it linear)
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        //change between full screen and non fullscreen
        Screen.fullScreen = isFullscreen;
    }
}

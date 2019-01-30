using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Script held by the empty object OptionMenu so all the functions are gathered all in one script
public class OptionMenu : MonoBehaviour {

    //Reference to AudioMixer object to manage exposed variables
    public AudioMixer mainMixer;

    //Array to store all the possible resolutions of the screen
    Resolution[] resolutions;

    //Reference to the Dropdown used for resolutions
    public Dropdown resolutionDropdown;

    //Fullscreen toggeling
//    public GameObject toggle;

    //Function called as the scene loads
    private void Start()
    {
        //Use the Screen object to recover all the possible resolution the screen can use
        resolutions = Screen.resolutions;

        //Flush all the default options contained by the Dropdown object
        resolutionDropdown.ClearOptions();

        //Creation of a temporary object to store the resolutions formated to be injected into the Dropdown object
        List<string> resolutionList = new List<string>();

        //Loop to format the resolutions so they can be injected into the Dropdown object
        int currentResolutionIndex = 0;
        for(int i=0 ; i < resolutions.Length; i++)
        {
            //The List of strings is injected each resolution with the format "width x height"
            resolutionList.Add(resolutions[i].width + " x " + resolutions[i].height);

            //Each resolution is checked until the one used by the application is found 
            //then the index of the resolution is stored for later use
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        //The List is injected into the Dropdown
        //then the selected item is changed to the one actualy used by the application
        //finaly the dropdown is refreshed to show the used one
        resolutionDropdown.AddOptions(resolutionList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        //Debug.Log(Screen.currentResolution);

//        toggle.active = Screen.fullScreen;
    }

    //Function called when the value of the slider is changed
    public void SetVolume(float volume)
    {
        //Log what happens for debug
        Debug.Log("Volume set to " + volume);
        //Change the exposed value "mainVolume"
        mainMixer.SetFloat("mainVolume",volume);
    }

    //Function called when fullscreen toggle is changed state
/*    public void ToggleFullScreen(bool toggle)
    {
        //Change fullscreen state depending on the state of the toggle button
        Screen.fullScreen = toggle;
         if (toggle)
          {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            
          }
          else
          {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            
          }
        //Screen.fullScreen = !Screen.fullScreen;
    }
*/
    //Function called when a resolution is selected in the dropdown
    public void SetResolution (int index)
    {
        //Resolution set to the one selected in the dropdown using the index of the selected resolution into the resolution array
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }
}

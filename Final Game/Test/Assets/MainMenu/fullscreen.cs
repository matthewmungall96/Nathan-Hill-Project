using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fullscreen : MonoBehaviour {

    public GameObject toggle;

    private void Start()
    {
       toggle.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    // Update is called once per frame
    public void Toggle () {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //Status of the menu
    public static bool pause = false;
    public bool inOptions = false;

    //Reference to the menu UI
    public GameObject pauseMenu;
    public GameObject optionMenu;


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (pause && !inOptions)
                Resume();
            else if (!pause && !inOptions)
                Pause();
	}

    public void QuitToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    private void Pause()
    {
        Debug.Log("Pausing");
        pauseMenu.SetActive(true);
        Time.timeScale=0F;
        pause = true;
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        pauseMenu.SetActive(false);
        Time.timeScale = 1F;
        pause = false;
    }

    public void EnterOptions()
    {
        inOptions = true;
    }

    public void LeaveOptions()
    {
        inOptions = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("menu");
    }
}

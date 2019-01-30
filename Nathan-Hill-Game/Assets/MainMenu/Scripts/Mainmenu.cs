using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script is held by the empty object MainMenu so functions are all gathered in one script
public class Mainmenu : MonoBehaviour {

    //Function called when Play button is pressed
	public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Function called when Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("menu");
    }

}

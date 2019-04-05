using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem.UnityGUI;

namespace PixelCrushers.DialogueSystem.Demo
{

    /// <summary>
    /// This script provides a rudimentary main menu for the Dialogue System's Demo.
    /// </summary>
    public class MainMenuNew : MonoBehaviour
    {

        [TextArea]
        public string startMessage = "Press Escape for Menu";
        public KeyCode menuKey = KeyCode.Escape;
        public GUISkin guiSkin;
        public bool closeWhenQuestLogOpen = true;

        public UnityEvent onOpen = new UnityEvent();
        public UnityEvent onClose = new UnityEvent();

        void Start()
        {
            
        }

        void Update()
        {
       
            // If you want to lock the cursor during gameplay, add ShowCursorOnConversation to the Player,
            // and uncomment the code below:
            //if (!DialogueManager.isConversationActive && !isMenuOpen && !IsQuestLogOpen ()) 
            //{
            //	Screen.lockCursor = true;
            //}
        }


        public void LoadGame()
        {
            PersistentDataManager.LevelWillBeUnloaded();
            var saveSystem = FindObjectOfType<SaveSystem>();
            if (saveSystem != null)
            {
                if (SaveSystem.HasSavedGameInSlot(1))
                {
                    SaveSystem.LoadFromSlot(1);
                    DialogueManager.ShowAlert("Game loaded.");
                }
                else
                {
                    DialogueManager.ShowAlert("Save a game first.");
                }
            }
            else
            {
                if (PlayerPrefs.HasKey("SavedGame"))
                {
                    string saveData = PlayerPrefs.GetString("SavedGame");
                    Debug.Log("Load Game Data: " + saveData);
                    LevelManager levelManager = FindObjectOfType<LevelManager>();
                    if (levelManager != null)
                    {
                        levelManager.LoadGame(saveData);
                    }
                    else
                    {
                        PersistentDataManager.ApplySaveData(saveData);
                        DialogueManager.SendUpdateTracker();
                    }
                    DialogueManager.ShowAlert("Game loaded.");
                }
                else
                {
                    DialogueManager.ShowAlert("Save a game first.");
                }
            }
        }


        public void NewGame()
        {
            if (PlayerPrefs.HasKey("SavedGame"))
            {
                PlayerPrefs.DeleteKey("SavedGame");
                Debug.Log("Cleared saved game data");
            }
            SceneManager.LoadScene("Tutorial (Testing)");
        }

    }

}

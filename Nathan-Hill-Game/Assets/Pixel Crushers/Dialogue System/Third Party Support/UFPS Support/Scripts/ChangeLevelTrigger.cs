using UnityEngine;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// This script changes to a different level, saving the Dialogue System's
    /// data before changing.
    /// </summary>
    [AddComponentMenu("")] // Deprecated but kept for compatibility with old projects.
    public class ChangeLevelTrigger : MonoBehaviour
    {

        public string newLevelName;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                string savegame = PixelCrushers.DialogueSystem.PersistentDataManager.GetSaveData();
                if (Debug.isDebugBuild) Debug.Log("Recording: " + savegame);
                Tools.LoadLevel(newLevelName);
            }
        }
    }

}

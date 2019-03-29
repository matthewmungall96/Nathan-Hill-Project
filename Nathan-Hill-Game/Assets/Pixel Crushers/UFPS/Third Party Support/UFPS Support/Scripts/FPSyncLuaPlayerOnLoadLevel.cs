using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// This component should usually be added to the Dialogue Manager object, not
    /// the player object! This is because it needs to persist across level loads.
    /// This component syncs the player's saved Lua data to the UFPS player components
    /// after a level has loaded. The player's position is not sync'ed, but stats
    /// and inventory are.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/UFPS/FP Sync Lua Player On Load Level (on Dialogue Manager)")]
    public class FPSyncLuaPlayerOnLoadLevel : MonoBehaviour
    {

        /// <summary>
        /// Set <c>true</c> to skip applying the player's saved info from Lua on the next
        /// level load. Normally you want to apply the saved info so the player's health,
        /// inventory, etc., persist across levels. However, if you're restarting the level
        /// with a fresh instance of the player (e.g., the player died and is respawning),
        /// set this <c>true</c> or the player will receive his old stats, such as zero health.
        /// </summary>
        public bool dontApplyLuaNextLoadLevel = false;

        /// <summary>
        /// Set <c>true</c> to include player position when applying Lua info, or 
        /// <c>false</c> to skip position.
        /// </summary>
        public bool includePlayerPosition = true;

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3)
        public void OnLevelWasLoaded(int level)
        {
            HandlePostLevelLoad();
        }
#else
        public virtual void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public virtual void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            HandlePostLevelLoad();
        }
#endif

        /// <summary>
        /// After a level is loaded, reapply the recorded persistent data from Lua.
        /// </summary>
        /// <param name="level">Level just loaded.</param>
        public virtual void HandlePostLevelLoad()
        {
            if (!dontApplyLuaNextLoadLevel) StartCoroutine(ApplyLuaAfterOneFrame());
            dontApplyLuaNextLoadLevel = false;
        }

        /// <summary>
        /// Applies the saved Lua data after one frame to give UFPS time to set up
        /// the player first. The player's position is not sync'ed from Lua.
        /// </summary>
        /// <returns>Coroutine enumerator.</returns>
        private IEnumerator ApplyLuaAfterOneFrame()
        {
            yield return null;
            var fpPersistentPlayerData = FindObjectOfType<FPPersistentPlayerData>();
            if (fpPersistentPlayerData != null)
            {
                bool originalRecordPosition = fpPersistentPlayerData.recordPosition;
                if (!includePlayerPosition) fpPersistentPlayerData.recordPosition = false;
                fpPersistentPlayerData.OnApplyPersistentData();
                fpPersistentPlayerData.recordPosition = originalRecordPosition;
            }
        }

    }

}

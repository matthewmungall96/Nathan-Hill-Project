using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// Add this script to your scene to re-tick
    /// FPPersistentPlayerData.DontApplyLuaNextLoadLevel.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/UFPS/FP Don't Apply Lua Next Load Level")]
    public class FPDontApplyLuaNextLoadLevel : MonoBehaviour
    {

        IEnumerator Start()
        {
            yield return null;
            yield return null;
            TickDontApplyLuaNextLoadLevel();
        }

        public void TickDontApplyLuaNextLoadLevel()
        {
            var sync = FindObjectOfType<FPSyncLuaPlayerOnLoadLevel>();
            if (sync != null)
            {
                sync.dontApplyLuaNextLoadLevel = true;
            }
        }
    }

}

using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    public static class UFPSMenuItems
    {

        [MenuItem("Tools/Pixel Crushers/Dialogue System/Third Party/UFPS/Add All Player Scripts")]
        public static void AddAllPlayerScripts()
        {
            var fpPlayer = GameObject.FindObjectOfType<vp_FPPlayerEventHandler>();
            if (fpPlayer == null)
            {
                Debug.LogError("Dialogue System: Can't find a UFPS player (vp_FPPlayerEventHandler) in the scene.");
            }
            else
            {
                Undo.RecordObject(fpPlayer.gameObject, "Add Dialogue System UFPS Scripts");
                if (fpPlayer.GetComponent<FPPlayerLuaBridge>() == null)
                {
                    fpPlayer.gameObject.AddComponent<FPPlayerLuaBridge>();
                }
                if (fpPlayer.GetComponent<FPPersistentPlayerData>() == null)
                {
                    fpPlayer.gameObject.AddComponent<FPPersistentPlayerData>();
                }
                if (fpPlayer.GetComponent<FPFreezePlayer>() == null)
                {
                    fpPlayer.gameObject.AddComponent<FPFreezePlayer>();
                }
                Debug.Log("Dialogue System: Added integration scripts to UFPS player.", fpPlayer);
            }
        }

    }

}

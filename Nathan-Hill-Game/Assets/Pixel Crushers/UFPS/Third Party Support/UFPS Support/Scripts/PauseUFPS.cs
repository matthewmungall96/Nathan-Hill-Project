using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// Use this utility script to freeze and unfreeze the player
    /// (and show/hide the cursor), for example when using the
    /// Dialogue System Menu Framework.
    /// </summary>
    public class PauseUFPS : MonoBehaviour
    {

        public void DoPause()
        {
            SetPlayerFreeze(true);
        }

        public void DoUnpause()
        {
            SetPlayerFreeze(false);
        }

        private void SetPlayerFreeze(bool value)
        {
            var fpFreezePlayer = FindObjectOfType<FPFreezePlayer>();
            if (fpFreezePlayer == null)
            {
                Debug.LogWarning("Can't find an FPFreezePlayer component to freeze/unfreeze the player!", this);
            }
            else if (value == true)
            {
                fpFreezePlayer.Freeze();
            }
            else
            {
                fpFreezePlayer.Unfreeze();
            }
        }

    }

}
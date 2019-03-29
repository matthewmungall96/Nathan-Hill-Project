using UnityEngine;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// Utility script to help debug cursor issues.
    /// </summary>
    public class DebugUFPSCursor : MonoBehaviour
    {
        private vp_FPInput m_fpInput = null;
        private vp_FPInput fpInput
        {
            get
            {
                if (m_fpInput == null) m_fpInput = FindObjectOfType<vp_FPInput>();
                return m_fpInput;
            }
        }

        private string m_text;
        private Rect m_rect = new Rect(0, 0, Screen.width, 30);

        private void Update()
        {
            m_text = "Cursor.visible=" + Cursor.visible + " Cursor.lockState=" + Cursor.lockState + " vp_Utility.LockCursor=" + vp_Utility.LockCursor +
                ((fpInput != null) ? " vp_FPInput.MouseCursorForced=" + fpInput.MouseCursorForced : string.Empty) +
                " Time.timeScale=" + Time.timeScale;
        }

        void OnGUI()
        {
            GUI.Label(m_rect, m_text);
        }
    }
}
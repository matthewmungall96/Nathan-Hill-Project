using UnityEngine;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// This component works with the PersistentDataManager and UFPS to keep track
    /// of a UFPS player's position, health, and inventory when saving and loading
    /// games. To use it, add it to the player object.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/UFPS/FP Persistent Player Data")]
    [RequireComponent(typeof(FPPlayerLuaBridge))]
    public class FPPersistentPlayerData : MonoBehaviour
    {

        /// <summary>
        /// Set <c>true</c> to record the player's position when saving persistent data, or
        /// <c>false</c> to skip it.
        /// </summary>
        [Tooltip("Record player's position.")]
        public bool recordPosition = true;

        /// <summary>
        /// Set <c>true</c> to force the player to wield the weapon after loading a game.
        /// Some custom weapons won't aim properly unless they've played the wield animation.
        /// </summary>
        [Tooltip("Force wield animation after loading game. Some custom weapons won't aim properly unless they've played the wield animation.")]
        public bool forceWield = true;

        private FPPlayerLuaBridge m_bridge = null;
        private FPPlayerLuaBridge bridge
        {
            get
            {
                if (m_bridge == null) m_bridge = GetComponent<FPPlayerLuaBridge>();
                return m_bridge;
            }
        }
        private vp_FPController m_fpController = null;
        private vp_FPController fpController
        {
            get
            {
                if (m_fpController == null) m_fpController = GetComponentInChildren<vp_FPController>();
                return m_fpController;
            }
        }
        private vp_FPCamera m_fpCamera = null;
        private vp_FPCamera fpCamera
        {
            get
            {
                if (m_fpCamera == null) m_fpCamera = GetComponentInChildren<vp_FPCamera>();
                return m_fpCamera;
            }
        }
        private string actorName
        {
            get { return OverrideActorName.GetPersistentDataName(transform); }
        }

        private void OnEnable()
        {
            PersistentDataManager.RegisterPersistentData(this.gameObject);
        }

        private void OnDisable()
        {
            PersistentDataManager.RegisterPersistentData(this.gameObject);
        }

        /// <summary>
        /// Listens for the OnRecordPersistentData message and records the game object's position 
        /// and rotation into the Lua Actor[] table.
        /// </summary>
        public void OnRecordPersistentData()
        {
            bridge.SyncFPToLua();
            if (recordPosition) RecordPositionToLua();
        }

        /// <summary>
        /// Listens for the OnApplyPersistentData message and retrieves the game object's position 
        /// and rotation from the Lua Actor[] table.
        /// </summary>
        public void OnApplyPersistentData()
        {
            bridge.forceWield = forceWield;
            bridge.SyncLuaToFP();
            if (recordPosition)
            {
                string spawnpointName = DialogueLua.GetActorField(actorName, "Spawnpoint").AsString;
                if (!string.IsNullOrEmpty(spawnpointName))
                {
                    if (DialogueDebug.LogInfo) Debug.Log("Dialogue System: Player spawnpoint: '" + spawnpointName + "'", this);
                    var spawnpoint = Tools.GameObjectHardFind(spawnpointName);
                    if (spawnpoint == null)
                    {
                        if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Persistent Position Data found Actor[" + actorName + "].Spawnpoint value '" + spawnpointName + "' but can't find a GameObject with this name in the scene. Moving actor to saved position instead.", this);
                    }
                    else
                    {
                        MovePlayerTo(spawnpoint.transform.position, spawnpoint.transform.rotation);
                        return;
                    }
                }
                ApplyLuaToPosition();
            }
        }

        private void RecordPositionToLua()
        {
            var positionString = GetPositionString();
            DialogueLua.SetActorField(actorName, "Position_" + PersistentPositionData.SanitizeLevelName(Tools.loadedLevelName), positionString);
            DialogueLua.SetActorField(actorName, "Position", positionString);
        }

        private void ApplyLuaToPosition()
        {
            var s = DialogueLua.GetActorField(actorName, "Position_" + PersistentPositionData.SanitizeLevelName(Tools.loadedLevelName)).AsString;
            if (string.IsNullOrEmpty(s)) s = DialogueLua.GetActorField(actorName, "Position").AsString; 
            ApplyPositionString(s);
        }

        private string GetPositionString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N4},{1:N4},{2:N4},{3:N4},{4:N4},{5:N4},{6:N4}",
                transform.position.x, transform.position.y, transform.position.z,
                transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }

        private void ApplyPositionString(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Equals("nil")) return;
            string[] tokens = s.Split(',');
            if (tokens.Length != 7) return;
            float[] values = new float[7];
            for (int i = 0; i < 7; i++)
            {
                values[i] = 0;
                float.TryParse(tokens[i], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out values[i]);
            }
            Vector3 pos = new Vector3(values[0], values[1], values[2]);
            Quaternion rot = new Quaternion(values[3], values[4], values[5], values[6]);
            MovePlayerTo(pos, rot);
        }

        private void MovePlayerTo(Vector3 pos, Quaternion rot)
        {
            if (DialogueDebug.LogInfo) Debug.Log("Dialogue System: Moving player to " + pos, this);
            if (fpController != null) fpController.SetPosition(pos);
            if (fpCamera != null) fpCamera.SetRotation(rot.eulerAngles);
        }

    }

}

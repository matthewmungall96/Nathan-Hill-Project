using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// This component provides methods to freeze and unfreeze the UFPS player. 
    /// It also freezes the player during conversations.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/Third Party/UFPS/FP Freeze Player")]
    [RequireComponent(typeof(vp_FPPlayerEventHandler))]
    public class FPFreezePlayer : MonoBehaviour
    {

        [Tooltip("Hide the UFPS HUD when freezing player.")]
        public bool hideHUD = true;

        [Tooltip("Freeze the player during conversations involving the player.")]
        public bool freezeDuringConversations = true;

        public bool disableInputOnFreeze = true;

        [Tooltip("When the scene starts, lock the cursor to enable mouse look without having to click in the window first.")]
        public bool lockCursorOnStart = true;

        [Tooltip("Lock the cursor when freezing the player.")]
        public bool lockCursorOnFreeze = true;

        public UnityEvent onFreeze = new UnityEvent();
        public UnityEvent onUnfreeze = new UnityEvent();

        // This delegate lets you provide a function that overrides when the cursor can be shown. For example,
        // if you're using a joystick, you never want to unlock (show) the cursor.
        public delegate bool CanShowCursorDelegate();
        public CanShowCursorDelegate CanShowCursor = null;

        private vp_FPPlayerEventHandler m_fpPlayerEventHandler = null;
        private vp_FPPlayerEventHandler fpPlayerEventHandler
        {
            get
            {
                if (m_fpPlayerEventHandler == null) m_fpPlayerEventHandler = GetComponentInChildren<vp_FPPlayerEventHandler>();
                return m_fpPlayerEventHandler;
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
        private vp_FPInput m_fpInput = null;
        private vp_FPInput fpInput
        {
            get
            {
                if (m_fpInput == null) m_fpInput = GetComponentInChildren<vp_FPInput>();
                return m_fpInput;
            }
        }
        private vp_SimpleHUD m_fpHUD = null;
        private vp_SimpleHUD fpHUD
        {
            get
            {
                if (m_fpHUD == null) m_fpHUD = GetComponentInChildren<vp_SimpleHUD>();
                return m_fpHUD;
            }
        }
        private vp_SimpleCrosshair m_fpCrosshair = null;
        private vp_SimpleCrosshair fpCrosshair
        {
            get
            {
                if (m_fpCrosshair == null) m_fpCrosshair = GetComponentInChildren<vp_SimpleCrosshair>();
                return m_fpCrosshair;
            }
        }

        private void Start()
        {
            if (lockCursorOnStart) StartCoroutine(LockCursorCoroutine());
        }

        /// <summary>
        /// When a conversation starts, freeze the UFPS player (i.e., gameplay) and show the cursor.
        /// </summary>
        /// <param name='actor'>
        /// Actor participating in the conversation.
        /// </param>
        public void OnConversationStart(Transform actor)
        {
            if (freezeDuringConversations) Freeze();
        }

        /// <summary>
        /// When a conversation ends, unfreeze the UFPS player (i.e., gameplay) and restore the 
        /// previous cursor state.
        /// </summary>
        /// <param name='actor'>
        /// Actor participating in the conversation.
        /// </param>
        public void OnConversationEnd(Transform actor)
        {
            if (freezeDuringConversations) Unfreeze();
        }

        /// <summary>
        /// Freeze the UFPS player (i.e., gameplay) and show the cursor.
        /// </summary>
        public void Freeze()
        {
            var canAlterCursorState = CanAlterCursorState();
            vp_LocalPlayer.Stop();
            vp_LocalPlayer.DisableGameplayInput();
            vp_LocalPlayer.DisableFreeLook();
            if (canAlterCursorState)
            {
                fpInput.MouseCursorForced = true;
                vp_Utility.LockCursor = false;
            }
            vp_LocalPlayer.HideCrosshair();
            if (hideHUD && (fpHUD != null)) fpHUD.enabled = false;
            onFreeze.Invoke();
        }

        /// <summary>
        /// Unfreeze the UFPS player (i.e. gameplay) and restore the previous cursor state.
        /// </summary>
        public void Unfreeze()
        {
            var canAlterCursorState = CanAlterCursorState() && lockCursorOnFreeze;
            vp_LocalPlayer.EnableGameplayInput();
            vp_LocalPlayer.EnableFreeLook();
            if (canAlterCursorState)
            {
                fpInput.MouseCursorForced = false;
                vp_Utility.LockCursor = true;
            }
            vp_LocalPlayer.ShowCrosshair();
            if (hideHUD && (fpHUD != null)) fpHUD.enabled = true;
            onUnfreeze.Invoke();
            if (canAlterCursorState) StartCoroutine(LockCursorCoroutine());
        }

        private bool CanAlterCursorState()
        {
            return (CanShowCursor != null) ? CanShowCursor() : true;
        }

        private IEnumerator LockCursorCoroutine()
        {
            var mouseMoved = false;
            var checkMouseTime = Time.time + 1;
            while (!(mouseMoved || fpInput.MouseCursorForced))
            {
                yield return null;
                if (Time.time > checkMouseTime) mouseMoved = Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05;
                if (!fpInput.MouseCursorForced)
                {
                    vp_Utility.LockCursor = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            if (!fpInput.MouseCursorForced)
            {
                vp_Utility.LockCursor = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

}

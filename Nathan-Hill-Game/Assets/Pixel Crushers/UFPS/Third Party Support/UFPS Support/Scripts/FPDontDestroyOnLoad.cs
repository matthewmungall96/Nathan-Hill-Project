using UnityEngine;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    /// <summary>
    /// Marks a GameObject as DontDestroyOnLoad.
    /// </summary>
    [AddComponentMenu("")] // Deprecated but kept for compatibility with old projects. Use DontDestroyGameObject instead.
    public class FPDontDestroyOnLoad : MonoBehaviour
    {

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }

    }

}
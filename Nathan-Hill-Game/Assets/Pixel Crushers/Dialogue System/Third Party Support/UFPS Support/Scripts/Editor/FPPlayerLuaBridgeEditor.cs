using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.UFPSSupport
{

    [CustomEditor(typeof(FPPlayerLuaBridge))]
    public class FPPlayerLuaBridgeEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var bridge = target as FPPlayerLuaBridge;
            if (GUILayout.Button(new GUIContent("Find All Item Types", "Fill the Usable Item Types list with all UFPS item types in the project.")))
            {
                if (EditorUtility.DisplayDialog("Find All Item Types", "This will fill the Usable Item Types list with all UFPS item types found in the project. Click OK to continue.", "OK", "Cancel"))
                {
                    Undo.RecordObject(target, "FPPlayerLuaBridge");
                    foreach (string guid in AssetDatabase.FindAssets("t:vp_ItemType"))
                    {
                        var itemType = AssetDatabase.LoadAssetAtPath<vp_ItemType>(AssetDatabase.GUIDToAssetPath(guid));
                        if (!bridge.usableItemTypes.Contains(itemType))
                        {
                            bridge.usableItemTypes.Add(itemType);
                        }
                    }
                }
            }
        }

    }

}

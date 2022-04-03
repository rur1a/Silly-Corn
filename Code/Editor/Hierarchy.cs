#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [ExecuteAlways]
    [InitializeOnLoad]
    public class Hierarchy : UnityEditor.Editor
    {
        private static Color s_defaultColor = new Color(0.2924528f, 0, 0.289029f);
        //private static Dictionary<int, Color> s_defaultColors = new Dictionary<int, Color>();
        static Hierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyGroup;
        }

        private static void HierarchyGroup(int instanceid, Rect selectionrect)
        {
            Object gameObject = EditorUtility.InstanceIDToObject(instanceid);
            if (gameObject == null || !gameObject.name.StartsWith("*")) return;

            EditorGUI.BeginChangeCheck();
            Color color = EditorGUI.ColorField(selectionrect, GUIContent.none, s_defaultColor, false, false, false);
            if (EditorGUI.EndChangeCheck())
            {
                s_defaultColor = color;
            }

            var guiStyle = new GUIStyle
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter
            };
            string text = ("<b><size=13><color=\"white\">" + gameObject.name.Remove(0,1) + "</color></size></b>");
            EditorGUI.LabelField(selectionrect, text, guiStyle);
        }
    }
}
#endif

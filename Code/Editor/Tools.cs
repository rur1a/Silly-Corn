using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        [MenuItem("Tools/Open/Open Characters")]
        public static void ShowCharacters() => 
            ShowFolder("Assets/_Project/Resources/Characters");

        [MenuItem("Tools/Open/Open LevelData")]
        public static void ShowLevelData() => 
            ShowFolder("Assets/_Project/Resources/StaticData/Levels");

        [MenuItem("Tools/Open/Open Player")]
        public static void ShowPlayer() => 
            ShowFolder("Assets/_Project/Resources/Player");
        
        [MenuItem("Tools/Open/Open Hand")]
        public static void ShowHand() => 
            ShowFolder("Assets/_Project/Resources/Hand");
        
        [MenuItem("Tools/Open/Open Menu")]
        public static void ShowMenu() => 
            ShowFolder("Assets/_Project/Resources/Hud");

        private static void ShowFolder(string path)
        {
            ShowFolderContents(AssetDatabase.LoadAssetAtPath<Object>(path).GetInstanceID());
        }

        private static void ShowFolderContents(int folderInstanceID)
        {
            Assembly editorAssembly = typeof(UnityEditor.Editor).Assembly;
            System.Type projectBrowserType = editorAssembly.GetType("UnityEditor.ProjectBrowser");
            MethodInfo showFolderContents = projectBrowserType.GetMethod(
                "ShowFolderContents", BindingFlags.Instance | BindingFlags.NonPublic);
            Object[] projectBrowserInstances = Resources.FindObjectsOfTypeAll(projectBrowserType);
         
            if (projectBrowserInstances.Length > 0)
            {
                for (int i = 0; i < projectBrowserInstances.Length; i++)
                    ShowFolderContentsInternal(projectBrowserInstances[i], showFolderContents, folderInstanceID);
            }
            else
            {
                EditorWindow projectBrowser = OpenNewProjectBrowser(projectBrowserType);
                ShowFolderContentsInternal(projectBrowser, showFolderContents, folderInstanceID);
            }
        }
         
        private static void ShowFolderContentsInternal(Object projectBrowser, MethodInfo showFolderContents, int folderInstanceID)
        {
            SerializedObject serializedObject = new SerializedObject(projectBrowser);
            bool inTwoColumnMode = serializedObject.FindProperty("m_ViewMode").enumValueIndex == 1;
         
            if (!inTwoColumnMode)
            {
                MethodInfo setTwoColumns = projectBrowser.GetType().GetMethod(
                    "SetTwoColumns", BindingFlags.Instance | BindingFlags.NonPublic);
                setTwoColumns.Invoke(projectBrowser, null);
            }
         
            bool revealAndFrameInFolderTree = true;
            showFolderContents.Invoke(projectBrowser, new object[] { folderInstanceID, revealAndFrameInFolderTree });
        }
         
        private static EditorWindow OpenNewProjectBrowser(System.Type projectBrowserType)
        {
            EditorWindow projectBrowser = EditorWindow.GetWindow(projectBrowserType);
            projectBrowser.Show();
            MethodInfo init = projectBrowserType.GetMethod("Init", BindingFlags.Instance | BindingFlags.Public);
            init.Invoke(projectBrowser, null);
            return projectBrowser;
        }
    }
}

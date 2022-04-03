using System;
using System.Linq;
using Code.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;
            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                UniqueId[] ids = FindObjectsOfType<UniqueId>();
                if (ids.Any(id => id != uniqueId && id.Id == uniqueId.Id))
                {
                    Generate(uniqueId);
                }
            }
        } 
        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}
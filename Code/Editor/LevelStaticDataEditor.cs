using System.Linq;
using Code.Infrastructure.StaticData;
using Code.Logic;
using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var levelData = (LevelStaticData)target;
            if (GUILayout.Button("GetAllSpawners"))
            {
                levelData.CharacterSpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new CharacterSpawnerData(x.GetComponent<UniqueId>().Id, x.Type, x.transform.position))
                    .ToList();
                levelData.PlayerPosition = GameObject.FindWithTag("SpawnPoint").transform.position;
                levelData.HandPosition = GameObject.FindWithTag("HandSpawnPoint").transform.position;
                levelData.LevelName = SceneManager.GetActiveScene().name;
            }
            EditorUtility.SetDirty(target);
        }
    }
}
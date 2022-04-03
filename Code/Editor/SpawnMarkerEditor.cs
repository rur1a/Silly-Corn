using System;
using Code.Logic;
using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void ShowGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = spawner.Type switch
            {
                CharacterType.Tomato => new Color32(160, 0, 0, 255),
                CharacterType.Eggplant => Color.magenta,
                CharacterType.Garlic => Color.white,
                CharacterType.Strawberry => Color.red,
                CharacterType.Onion => Color.yellow,
                _ => throw new ArgumentOutOfRangeException()
            };
            Gizmos.DrawSphere(spawner.transform.position, 1);
        }
    }
}

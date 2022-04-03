using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelName;
        public Vector3 PlayerPosition;
        public Vector3 HandPosition;
        public List<CharacterSpawnerData> CharacterSpawners;
    }
}
using System;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [Serializable]
    public class CharacterSpawnerData
    {
        public string Id;
        public CharacterType Type;
        public Vector3 Position;

        public CharacterSpawnerData(string id, CharacterType type, Vector3 position)
        {
            Id = id;
            Type = type;
            Position = position;
        }
    }
}
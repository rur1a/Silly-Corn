using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "StaticData/Character", order = 0)]
    public class CharacterStaticData : ScriptableObject
    {
        public CharacterType CharacterType;
        public GameObject Prefab;
    }
}
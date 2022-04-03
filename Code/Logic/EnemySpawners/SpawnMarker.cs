using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public CharacterType Type { get; set; }
    }
}
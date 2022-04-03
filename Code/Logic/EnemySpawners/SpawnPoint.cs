using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [field: SerializeField] public CharacterType Type { get; set; }
        public string Id { get; set; }
        public bool Grab;
        private IGameFactory _gameFactory;
        private Character.Character _character;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private GameObject Spawn()
        {
            GameObject character = _gameFactory.CreateCharacter(Type, transform);
            character.transform.Rotate(Vector3.up, 180);
            _character = character.GetComponent<Character.Character>();
            _character.Grabbed += Kill;
            return character;
        }

        private void Kill()
        {
            Grab = true;
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.GrabData.GrabbedCharacters.Contains(Id))
                Grab = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if(Grab)
                progress.GrabData.GrabbedCharacters.Add(Id);
        }
    }
}
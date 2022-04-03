using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<CharacterType, CharacterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _monsters = Resources.LoadAll<CharacterStaticData>("StaticData/Characters")
                .ToDictionary(x => x.CharacterType, x => x);
            
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelName, x => x);
        }

        public CharacterStaticData ForCharacter(CharacterType type) => 
            _monsters.TryGetValue(type, out CharacterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneName) =>
            _levels.TryGetValue(sceneName, out LevelStaticData staticData) ? staticData : null;
    }
}
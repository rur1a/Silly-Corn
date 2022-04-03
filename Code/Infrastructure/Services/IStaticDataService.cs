using Code.Infrastructure.StaticData;
using Code.Logic;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void Load();
        CharacterStaticData ForCharacter(CharacterType type);
        LevelStaticData ForLevel(string sceneName);
    }
}
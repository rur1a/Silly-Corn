using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public GrabData GrabData;

        public PlayerProgress(string levelName)
        {
            WorldData = new WorldData(levelName);
            GrabData = new GrabData();
        }
    }
}
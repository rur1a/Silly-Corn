using System;

namespace Code.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public State State;

        public WorldData(string levelName)
        {
            PositionOnLevel = new PositionOnLevel(levelName);
            State = new State();
        }
    }
}
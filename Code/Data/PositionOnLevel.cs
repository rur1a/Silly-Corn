using System;

namespace Code.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string LevelName;
        public Vector3Data Position;

        public PositionOnLevel(string levelName, Vector3Data position)
        {
            LevelName = levelName;
            Position = position;
        }

        public PositionOnLevel(string levelName)
        {
            LevelName = levelName;
        }
    }
}
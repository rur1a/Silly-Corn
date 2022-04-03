using System;

namespace Code.Data
{
    [Serializable]
    public class State
    {
        public int MaxHP;
        public int CurrentHp;

        public void ResetHP() =>
            CurrentHp = MaxHP;
    }
}
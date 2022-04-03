using System;

namespace Code.Character.Health
{
    public interface IHealth
    {
        event Action HealthChanged;
        int Current { get; set; }
        int Max { get; set; }
        void TakeDamage(int damage);
        void Kill();
    }
}
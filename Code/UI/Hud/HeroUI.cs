using Code.Character.Health;
using UnityEngine;

namespace Code.UI.Hud
{
    public class HeroUI : MonoBehaviour
    {
        private HpBar _hpBar;
        private IHealth _health;

        public void Construct(IHealth health, HpBar hpBar)
        {
            _health = health;
            _hpBar = hpBar;
        }

        private void Start() => 
            _health.HealthChanged += UpdateHpBar;

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() => 
            _hpBar.SetValue(_health.Current, _health.Max);
    }
}
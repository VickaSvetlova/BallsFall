using System;
using UnityEngine;

namespace BallsFall
{
    public class Health : MonoBehaviour
    {
        public event Action<(float, float)> OnChangeHealth;
        public event Action OnDead;
        private float currentHealth;
        private float maxHealth;

        public void SetHealth((float, float) health)
        {
            currentHealth = health.Item1;
            maxHealth = health.Item2;
            OnChangeHealth?.Invoke((currentHealth, maxHealth));
        }

        public (float, float) GetHealth()
        {
            OnChangeHealth?.Invoke((currentHealth, maxHealth));
            return (currentHealth, maxHealth);
        }

        public void ExtactLife(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDead?.Invoke();
            }

            OnChangeHealth?.Invoke((currentHealth, maxHealth));
        }
    }
}
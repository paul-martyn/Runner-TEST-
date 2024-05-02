using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerStats
    {
        public int HealthCount;
        public float MovementSpeed;

        public PlayerStats(int healthCount, float movementSpeed)
        {
            HealthCount = healthCount;
            MovementSpeed = movementSpeed;
        }
    }
}
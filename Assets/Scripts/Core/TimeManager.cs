using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float tickInterval = 0.5f;
        [SerializeField] private int speedMultiplier = 1;

        private float elapsed;

        public float TickInterval => tickInterval;
        public int SpeedMultiplier => speedMultiplier;

        public bool ConsumeTick(float deltaTime)
        {
            elapsed += deltaTime * speedMultiplier;
            if (elapsed < tickInterval)
            {
                return false;
            }

            elapsed -= tickInterval;
            return true;
        }

        public void SetSpeed(int newMultiplier)
        {
            speedMultiplier = Mathf.Max(1, newMultiplier);
        }
    }
}
using UnityEngine;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.NPC
{
    public class NpcInventory : MonoBehaviour
    {
        [SerializeField] private int food;
        [SerializeField] private int wood;
        [SerializeField] private int stone;

        public int Food => food;
        public int Wood => wood;
        public int Stone => stone;

        public void Add(ResourceKind kind, int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            switch (kind)
            {
                case ResourceKind.Food:
                    food += amount;
                    break;
                case ResourceKind.Wood:
                    wood += amount;
                    break;
                case ResourceKind.Stone:
                    stone += amount;
                    break;
            }
        }
    }
}
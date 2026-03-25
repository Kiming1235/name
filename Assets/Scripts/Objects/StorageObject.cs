using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class StorageObject : WorldObject
    {
        private static readonly List<StorageObject> ActiveStorages = new();

        [SerializeField] private int storedFood;
        [SerializeField] private int storedWood;
        [SerializeField] private int storedStone;

        public int StoredFood => storedFood;
        public int StoredWood => storedWood;
        public int StoredStone => storedStone;

        private void OnEnable()
        {
            if (!ActiveStorages.Contains(this))
            {
                ActiveStorages.Add(this);
            }
        }

        private void OnDisable()
        {
            ActiveStorages.Remove(this);
        }

        public void Deposit(ResourceKind kind, int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            switch (kind)
            {
                case ResourceKind.Food:
                    storedFood += amount;
                    break;
                case ResourceKind.Wood:
                    storedWood += amount;
                    break;
                case ResourceKind.Stone:
                    storedStone += amount;
                    break;
            }
        }

        public static StorageObject FindClosest(Vector2Int origin)
        {
            StorageObject best = null;
            int bestDistance = int.MaxValue;

            foreach (StorageObject storage in ActiveStorages)
            {
                if (storage == null)
                {
                    continue;
                }

                int distance = Mathf.Abs(storage.GridPosition.x - origin.x) + Mathf.Abs(storage.GridPosition.y - origin.y);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = storage;
                }
            }

            return best;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class HouseResidence : MonoBehaviour
    {
        private static readonly List<HouseResidence> ActiveResidences = new();

        [SerializeField] private HouseObject houseObject;
        [SerializeField] private Vector2Int fallbackGridPosition;
        [SerializeField] private int capacity = 4;
        [SerializeField] private List<string> residentNpcIds = new();

        public Vector2Int GridPosition => houseObject != null ? houseObject.GridPosition : fallbackGridPosition;
        public bool HasSpace => residentNpcIds.Count < capacity;

        private void Awake()
        {
            if (houseObject == null)
            {
                houseObject = GetComponent<HouseObject>();
            }
        }

        private void OnEnable()
        {
            if (!ActiveResidences.Contains(this))
            {
                ActiveResidences.Add(this);
            }
        }

        private void OnDisable()
        {
            ActiveResidences.Remove(this);
        }

        public bool EnsureResident(string npcId)
        {
            if (string.IsNullOrWhiteSpace(npcId))
            {
                return false;
            }

            if (residentNpcIds.Contains(npcId))
            {
                return true;
            }

            if (!HasSpace)
            {
                return false;
            }

            residentNpcIds.Add(npcId);
            return true;
        }

        public static HouseResidence FindClosest(Vector2Int origin)
        {
            HouseResidence best = null;
            int bestDistance = int.MaxValue;

            foreach (HouseResidence residence in ActiveResidences)
            {
                if (residence == null)
                {
                    continue;
                }

                int distance = Mathf.Abs(residence.GridPosition.x - origin.x) + Mathf.Abs(residence.GridPosition.y - origin.y);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = residence;
                }
            }

            return best;
        }

        public static HouseResidence FindClosestAvailable(Vector2Int origin, string npcId)
        {
            HouseResidence best = null;
            int bestDistance = int.MaxValue;

            foreach (HouseResidence residence in ActiveResidences)
            {
                if (residence == null)
                {
                    continue;
                }

                bool canUse = residence.residentNpcIds.Contains(npcId) || residence.HasSpace;
                if (!canUse)
                {
                    continue;
                }

                int distance = Mathf.Abs(residence.GridPosition.x - origin.x) + Mathf.Abs(residence.GridPosition.y - origin.y);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = residence;
                }
            }

            return best;
        }
    }
}
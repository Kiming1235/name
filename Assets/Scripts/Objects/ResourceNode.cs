using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public enum ResourceKind
    {
        Food,
        Wood,
        Stone
    }

    public class ResourceNode : MonoBehaviour
    {
        private static readonly List<ResourceNode> ActiveNodes = new();

        [SerializeField] private ResourceKind resourceKind;
        [SerializeField] private int currentAmount = 10;
        [SerializeField] private int harvestPerAction = 1;
        [SerializeField] private Vector2Int fallbackGridPosition;

        private WorldObject cachedWorldObject;

        public ResourceKind Kind => resourceKind;
        public int CurrentAmount => currentAmount;
        public Vector2Int GridPosition => cachedWorldObject != null ? cachedWorldObject.GridPosition : fallbackGridPosition;
        public bool CanHarvest => currentAmount > 0;

        private void Awake()
        {
            cachedWorldObject = GetComponent<WorldObject>();
        }

        private void OnEnable()
        {
            if (!ActiveNodes.Contains(this))
            {
                ActiveNodes.Add(this);
            }
        }

        private void OnDisable()
        {
            ActiveNodes.Remove(this);
        }

        public int HarvestOnce()
        {
            if (currentAmount <= 0)
            {
                return 0;
            }

            int harvested = Mathf.Min(harvestPerAction, currentAmount);
            currentAmount -= harvested;
            return harvested;
        }

        public static ResourceNode FindClosestHarvestable(ResourceKind kind, Vector2Int origin)
        {
            ResourceNode best = null;
            int bestDistance = int.MaxValue;

            foreach (ResourceNode node in ActiveNodes)
            {
                if (node == null || !node.CanHarvest || node.resourceKind != kind)
                {
                    continue;
                }

                int distance = Mathf.Abs(node.GridPosition.x - origin.x) + Mathf.Abs(node.GridPosition.y - origin.y);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = node;
                }
            }

            return best;
        }

        public static ResourceNode FindClosestAny(Vector2Int origin)
        {
            ResourceNode best = null;
            int bestDistance = int.MaxValue;

            foreach (ResourceNode node in ActiveNodes)
            {
                if (node == null || !node.CanHarvest)
                {
                    continue;
                }

                int distance = Mathf.Abs(node.GridPosition.x - origin.x) + Mathf.Abs(node.GridPosition.y - origin.y);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = node;
                }
            }

            return best;
        }
    }
}
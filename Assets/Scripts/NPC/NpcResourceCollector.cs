using UnityEngine;
using QuarterVillageSim.Objects;
using QuarterVillageSim.Core;

namespace QuarterVillageSim.NPC
{
    public class NpcResourceCollector : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private NpcInventory inventory;
        [SerializeField] private EventLogManager eventLogManager;
        [SerializeField] private float actionInterval = 1f;

        private float elapsed;
        private ResourceNode currentTarget;

        private void Awake()
        {
            if (npcController == null)
            {
                npcController = GetComponent<NpcController>();
            }

            if (inventory == null)
            {
                inventory = GetComponent<NpcInventory>();
            }

            if (eventLogManager == null)
            {
                eventLogManager = FindObjectOfType<EventLogManager>();
            }
        }

        private void Update()
        {
            if (npcController == null || inventory == null)
            {
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed < actionInterval)
            {
                return;
            }

            elapsed = 0f;
            TickCollector();
        }

        private void TickCollector()
        {
            if (currentTarget == null || !currentTarget.CanHarvest)
            {
                currentTarget = ResourceNode.FindClosestAny(npcController.Data.GridPosition);
            }

            if (currentTarget == null)
            {
                return;
            }

            Vector2Int npcPos = npcController.Data.GridPosition;
            Vector2Int targetPos = currentTarget.GridPosition;
            int distance = Mathf.Abs(npcPos.x - targetPos.x) + Mathf.Abs(npcPos.y - targetPos.y);

            if (distance > 1)
            {
                MoveOneStepToward(targetPos);
                npcController.Data.CurrentState = NpcState.Moving;
                return;
            }

            int harvested = currentTarget.HarvestOnce();
            if (harvested <= 0)
            {
                return;
            }

            inventory.Add(currentTarget.Kind, harvested);
            npcController.Data.CurrentState = GetHarvestState(currentTarget.Kind);
            eventLogManager?.AddLog($"{npcController.Data.DisplayName} 가 {currentTarget.Kind} 자원 {harvested} 획득");
        }

        private void MoveOneStepToward(Vector2Int targetPos)
        {
            Vector2Int current = npcController.Data.GridPosition;
            int nextX = current.x;
            int nextY = current.y;

            if (targetPos.x != current.x)
            {
                nextX += targetPos.x > current.x ? 1 : -1;
            }
            else if (targetPos.y != current.y)
            {
                nextY += targetPos.y > current.y ? 1 : -1;
            }

            npcController.Data.GridPosition = new Vector2Int(nextX, nextY);
        }

        private NpcState GetHarvestState(ResourceKind kind)
        {
            switch (kind)
            {
                case ResourceKind.Wood:
                    return NpcState.Chopping;
                case ResourceKind.Stone:
                    return NpcState.Mining;
                case ResourceKind.Food:
                default:
                    return NpcState.Gathering;
            }
        }
    }
}
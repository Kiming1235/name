using UnityEngine;
using QuarterVillageSim.Core;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.NPC
{
    public class NpcPriorityResourceCollector : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private NpcInventory inventory;
        [SerializeField] private EventLogManager eventLogManager;
        [SerializeField] private float actionInterval = 1f;
        [SerializeField] private int depositThreshold = 3;
        [SerializeField] private float hungerFoodPriorityThreshold = 70f;

        private float elapsed;
        private ResourceNode currentTarget;
        private StorageObject currentStorage;

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
            if (ShouldDeposit())
            {
                HandleDeposit();
                return;
            }

            HandleHarvest();
        }

        private bool ShouldDeposit()
        {
            return NpcInventoryAccess.GetTotalAmount(inventory) >= depositThreshold;
        }

        private void HandleDeposit()
        {
            if (currentStorage == null)
            {
                currentStorage = StorageObject.FindClosest(npcController.Data.GridPosition);
            }

            if (currentStorage == null)
            {
                return;
            }

            Vector2Int npcPos = npcController.Data.GridPosition;
            Vector2Int targetPos = currentStorage.GridPosition;
            int distance = Mathf.Abs(npcPos.x - targetPos.x) + Mathf.Abs(npcPos.y - targetPos.y);

            if (distance > 1)
            {
                MoveOneStepToward(targetPos);
                npcController.Data.CurrentState = NpcState.Carrying;
                return;
            }

            DepositAllResources();
            currentStorage = null;
        }

        private void DepositAllResources()
        {
            DepositOneKind(ResourceKind.Food);
            DepositOneKind(ResourceKind.Wood);
            DepositOneKind(ResourceKind.Stone);
        }

        private void DepositOneKind(ResourceKind kind)
        {
            int amount = NpcInventoryAccess.GetAmount(inventory, kind);
            if (amount <= 0 || currentStorage == null)
            {
                return;
            }

            currentStorage.Deposit(kind, amount);
            NpcInventoryAccess.Remove(inventory, kind, amount);
            eventLogManager?.AddLog($"{npcController.Data.DisplayName} 가 저장고에 {kind} 자원 {amount} 입고");
        }

        private void HandleHarvest()
        {
            if (currentTarget == null || !currentTarget.CanHarvest)
            {
                currentTarget = FindPriorityTarget();
            }

            if (currentTarget == null)
            {
                npcController.Data.CurrentState = NpcState.Idle;
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
                currentTarget = null;
                return;
            }

            inventory.Add(currentTarget.Kind, harvested);
            npcController.Data.CurrentState = GetHarvestState(currentTarget.Kind);
            eventLogManager?.AddLog($"{npcController.Data.DisplayName} 가 {currentTarget.Kind} 자원 {harvested} 획득");
        }

        private ResourceNode FindPriorityTarget()
        {
            if (npcController.Data.Needs.Hunger >= hungerFoodPriorityThreshold)
            {
                ResourceNode foodTarget = ResourceNode.FindClosestHarvestable(ResourceKind.Food, npcController.Data.GridPosition);
                if (foodTarget != null)
                {
                    return foodTarget;
                }
            }

            return ResourceNode.FindClosestAny(npcController.Data.GridPosition);
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
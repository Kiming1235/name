using UnityEngine;
using QuarterVillageSim.Core;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.NPC
{
    public class NpcSleepController : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private NpcHomeBinding homeBinding;
        [SerializeField] private EventLogManager eventLogManager;
        [SerializeField] private float actionInterval = 1f;
        [SerializeField] private float fatigueSleepThreshold = 75f;
        [SerializeField] private float fatigueRecoveryPerTick = 12f;
        [SerializeField] private float hungerIncreaseWhileSleeping = 0.5f;

        private float elapsed;

        private void Awake()
        {
            if (npcController == null)
            {
                npcController = GetComponent<NpcController>();
            }

            if (homeBinding == null)
            {
                homeBinding = GetComponent<NpcHomeBinding>();
            }

            if (eventLogManager == null)
            {
                eventLogManager = FindObjectOfType<EventLogManager>();
            }
        }

        private void Start()
        {
            homeBinding?.EnsureHomeAssigned();
        }

        private void Update()
        {
            if (npcController == null || homeBinding == null)
            {
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed < actionInterval)
            {
                return;
            }

            elapsed = 0f;
            TickSleep();
        }

        private void TickSleep()
        {
            if (npcController.Data.Needs.Fatigue < fatigueSleepThreshold && npcController.Data.CurrentState != NpcState.Sleeping)
            {
                return;
            }

            homeBinding.EnsureHomeAssigned();
            HouseResidence home = homeBinding.AssignedHome;
            if (home == null)
            {
                return;
            }

            Vector2Int current = npcController.Data.GridPosition;
            Vector2Int target = home.GridPosition;
            int distance = Mathf.Abs(current.x - target.x) + Mathf.Abs(current.y - target.y);

            if (distance > 0)
            {
                MoveOneStepToward(target);
                npcController.Data.CurrentState = NpcState.Moving;
                return;
            }

            npcController.Data.CurrentState = NpcState.Sleeping;
            npcController.Data.Needs.Fatigue = Mathf.Max(0f, npcController.Data.Needs.Fatigue - fatigueRecoveryPerTick);
            npcController.Data.Needs.Hunger += hungerIncreaseWhileSleeping;
            eventLogManager?.AddLog($"{npcController.Data.DisplayName} 가 집에서 수면 중");
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
    }
}
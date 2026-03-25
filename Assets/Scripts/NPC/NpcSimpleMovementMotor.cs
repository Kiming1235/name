using UnityEngine;

namespace QuarterVillageSim.NPC
{
    public class NpcSimpleMovementMotor : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private float moveInterval = 1f;
        [SerializeField] private int moveRange = 1;

        private float elapsed;

        private void Awake()
        {
            if (npcController == null)
            {
                npcController = GetComponent<NpcController>();
            }
        }

        private void Update()
        {
            if (npcController == null)
            {
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed < moveInterval)
            {
                return;
            }

            elapsed = 0f;
            TryMoveRandomly();
        }

        private void TryMoveRandomly()
        {
            if (npcController.Data.CurrentState != NpcState.Moving)
            {
                return;
            }

            int deltaX = Random.Range(-moveRange, moveRange + 1);
            int deltaY = Random.Range(-moveRange, moveRange + 1);
            if (deltaX == 0 && deltaY == 0)
            {
                deltaX = 1;
            }

            npcController.Data.GridPosition += new Vector2Int(deltaX, deltaY);
        }
    }
}
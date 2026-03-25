using UnityEngine;
using QuarterVillageSim.Core;

namespace QuarterVillageSim.NPC
{
    public class NpcGridPositionSync : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private bool updateEveryFrame = true;

        private void Awake()
        {
            if (npcController == null)
            {
                npcController = GetComponent<NpcController>();
            }
        }

        private void LateUpdate()
        {
            if (!updateEveryFrame)
            {
                return;
            }

            SyncNow();
        }

        public void SyncNow()
        {
            if (npcController == null || gridManager == null)
            {
                return;
            }

            transform.position = gridManager.GridToWorld(npcController.Data.GridPosition);
        }

        public void SetGridManager(GridManager manager)
        {
            gridManager = manager;
        }
    }
}
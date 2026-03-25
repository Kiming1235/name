using UnityEngine;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.NPC
{
    public class NpcHomeBinding : MonoBehaviour
    {
        [SerializeField] private NpcController npcController;
        [SerializeField] private HouseResidence assignedHome;

        public HouseResidence AssignedHome => assignedHome;

        private void Awake()
        {
            if (npcController == null)
            {
                npcController = GetComponent<NpcController>();
            }
        }

        public void EnsureHomeAssigned()
        {
            if (npcController == null)
            {
                return;
            }

            if (assignedHome != null && assignedHome.EnsureResident(npcController.Data.Id))
            {
                return;
            }

            assignedHome = HouseResidence.FindClosestAvailable(npcController.Data.GridPosition, npcController.Data.Id);
            if (assignedHome != null)
            {
                assignedHome.EnsureResident(npcController.Data.Id);
            }
        }
    }
}
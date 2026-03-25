using UnityEngine;
using QuarterVillageSim.AI;

namespace QuarterVillageSim.NPC
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private NpcData npcData = new();
        [SerializeField] private NpcBrain npcBrain;

        public NpcData Data => npcData;

        public void Initialize()
        {
            if (npcBrain == null)
            {
                npcBrain = GetComponent<NpcBrain>();
            }
        }

        public void TickNpc()
        {
            UpdateNeeds();
            npcBrain?.EvaluateAndAssignAction(this);
        }

        private void UpdateNeeds()
        {
            npcData.Needs.Hunger += 1f;
            npcData.Needs.Fatigue += 0.5f;
            npcData.Needs.Safety = Mathf.Clamp(npcData.Needs.Safety, 0f, 100f);
        }
    }
}
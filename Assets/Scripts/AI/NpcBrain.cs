using UnityEngine;
using QuarterVillageSim.NPC;

namespace QuarterVillageSim.AI
{
    public class NpcBrain : MonoBehaviour
    {
        public void EvaluateAndAssignAction(NpcController controller)
        {
            if (controller == null)
            {
                return;
            }

            var data = controller.Data;
            if (data.Needs.Hunger > 80f)
            {
                data.CurrentState = NpcState.Eating;
                return;
            }

            if (data.Needs.Fatigue > 80f)
            {
                data.CurrentState = NpcState.Sleeping;
                return;
            }

            data.CurrentState = NpcState.Moving;
        }
    }
}
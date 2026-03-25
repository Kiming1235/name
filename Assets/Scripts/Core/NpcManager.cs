using System.Collections.Generic;
using UnityEngine;
using QuarterVillageSim.NPC;

namespace QuarterVillageSim.Core
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private List<NpcController> npcControllers = new();

        public void InitializeNpcs()
        {
            foreach (var npc in npcControllers)
            {
                if (npc == null)
                {
                    continue;
                }

                npc.Initialize();
            }
        }

        public void TickAllNpcs()
        {
            foreach (var npc in npcControllers)
            {
                if (npc == null)
                {
                    continue;
                }

                npc.TickNpc();
            }
        }
    }
}
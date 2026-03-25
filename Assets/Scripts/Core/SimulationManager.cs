using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class SimulationManager : MonoBehaviour
    {
        private TimeManager timeManager;
        private WorldManager worldManager;
        private NpcManager npcManager;
        private EventLogManager eventLogManager;

        public void Initialize(
            TimeManager timeManagerRef,
            WorldManager worldManagerRef,
            NpcManager npcManagerRef,
            EventLogManager eventLogManagerRef)
        {
            timeManager = timeManagerRef;
            worldManager = worldManagerRef;
            npcManager = npcManagerRef;
            eventLogManager = eventLogManagerRef;
        }

        private void Update()
        {
            if (timeManager == null || npcManager == null)
            {
                return;
            }

            if (!timeManager.ConsumeTick(Time.deltaTime))
            {
                return;
            }

            RunSimulationTick();
        }

        private void RunSimulationTick()
        {
            npcManager.TickAllNpcs();
            worldManager.TickWorldObjects();
            eventLogManager.FlushTickSummary();
        }
    }
}
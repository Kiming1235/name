using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Manager References")]
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private SimulationManager simulationManager;
        [SerializeField] private WorldManager worldManager;
        [SerializeField] private NpcManager npcManager;
        [SerializeField] private EventLogManager eventLogManager;

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            worldManager.InitializeWorld();
            npcManager.InitializeNpcs();
            simulationManager.Initialize(timeManager, worldManager, npcManager, eventLogManager);
        }

        private void ValidateReferences()
        {
            if (timeManager == null) Debug.LogWarning("TimeManager reference is missing.");
            if (simulationManager == null) Debug.LogWarning("SimulationManager reference is missing.");
            if (worldManager == null) Debug.LogWarning("WorldManager reference is missing.");
            if (npcManager == null) Debug.LogWarning("NpcManager reference is missing.");
            if (eventLogManager == null) Debug.LogWarning("EventLogManager reference is missing.");
        }
    }
}
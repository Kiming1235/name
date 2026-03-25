using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using QuarterVillageSim.Core;
using QuarterVillageSim.Data;

namespace QuarterVillageSim.NPC
{
    [DefaultExecutionOrder(-100)]
    public class SpikyBootstrapper : MonoBehaviour
    {
        [SerializeField] private SpikyNpcSpawner spawner;
        [SerializeField] private NpcManager npcManager;
        [SerializeField] private GridManager gridManager;

        private static readonly FieldInfo NpcControllersField = typeof(NpcManager)
            .GetField("npcControllers", BindingFlags.Instance | BindingFlags.NonPublic);

        private void Awake()
        {
            if (spawner == null || npcManager == null)
            {
                return;
            }

            List<NpcController> spawned = spawner.SpawnAll();
            PrepareSpawnedNpcs(spawned);
            RegisterSpawnedNpcs(spawned);
        }

        private void PrepareSpawnedNpcs(List<NpcController> spawned)
        {
            foreach (NpcController npc in spawned)
            {
                if (npc == null)
                {
                    continue;
                }

                ApplyFallbackPresetIfNeeded(npc);

                NpcGridPositionSync sync = npc.GetComponent<NpcGridPositionSync>();
                if (sync != null)
                {
                    sync.SetGridManager(gridManager);
                    sync.SyncNow();
                }
            }
        }

        private void ApplyFallbackPresetIfNeeded(NpcController npc)
        {
            if (npc.Data.Personality.Tags.Count > 0)
            {
                return;
            }

            PersonalityPreset preset = SpikyFallbackPresets.GetRandomPreset();
            npc.Data.Personality.Tags = new List<string>(preset.Tags);
            npc.Data.Values.Values = new List<string>(preset.Values);
        }

        private void RegisterSpawnedNpcs(List<NpcController> spawned)
        {
            if (NpcControllersField == null)
            {
                Debug.LogWarning("NpcManager private list field was not found.");
                return;
            }

            NpcControllersField.SetValue(npcManager, spawned);
        }
    }
}
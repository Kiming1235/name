using System.Collections.Generic;
using UnityEngine;
using QuarterVillageSim.Data;

namespace QuarterVillageSim.NPC
{
    public class SpikyNpcSpawner : MonoBehaviour
    {
        [SerializeField] private NpcController npcPrefab;
        [SerializeField] private PersonalityPresetDatabase personalityPresetDatabase;
        [SerializeField] private Transform npcRoot;
        [SerializeField] private List<Vector2Int> spawnPositions = new();

        private static readonly string[] DisplayNames =
        {
            "스피키 A",
            "스피키 B",
            "스피키 C",
            "스피키 D"
        };

        private static readonly string[] InternalIds =
        {
            "spiky_a",
            "spiky_b",
            "spiky_c",
            "spiky_d"
        };

        public List<NpcController> SpawnAll()
        {
            var spawned = new List<NpcController>();
            if (npcPrefab == null)
            {
                return spawned;
            }

            for (int i = 0; i < DisplayNames.Length; i++)
            {
                var instance = Instantiate(npcPrefab, npcRoot);
                var data = instance.Data;
                data.Id = InternalIds[i];
                data.DisplayName = DisplayNames[i];
                data.GridPosition = i < spawnPositions.Count ? spawnPositions[i] : new Vector2Int(i, 0);

                var preset = personalityPresetDatabase != null ? personalityPresetDatabase.GetRandomPreset() : null;
                if (preset != null)
                {
                    data.Personality.Tags = new List<string>(preset.Tags);
                    data.Values.Values = new List<string>(preset.Values);
                }

                instance.transform.position = new Vector3(data.GridPosition.x, data.GridPosition.y, 0f);
                instance.Initialize();
                spawned.Add(instance);
            }

            return spawned;
        }
    }
}
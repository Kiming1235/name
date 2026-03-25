using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Data
{
    [CreateAssetMenu(menuName = "QuarterVillageSim/Personality Preset Database")]
    public class PersonalityPresetDatabase : ScriptableObject
    {
        [SerializeField] private List<PersonalityPreset> presets = new();

        public PersonalityPreset GetRandomPreset()
        {
            if (presets.Count == 0)
            {
                return null;
            }

            return presets[Random.Range(0, presets.Count)];
        }
    }

    [System.Serializable]
    public class PersonalityPreset
    {
        public string PresetName;
        public List<string> Tags = new();
        public List<string> Values = new();
    }
}
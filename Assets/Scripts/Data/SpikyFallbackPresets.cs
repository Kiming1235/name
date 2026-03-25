using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Data
{
    public static class SpikyFallbackPresets
    {
        private static readonly List<PersonalityPreset> Presets = new()
        {
            new PersonalityPreset
            {
                PresetName = "신중형",
                Tags = new List<string> { "신중함", "겁 많음", "공감적" },
                Values = new List<string> { "안전 우선", "공동체 중시" }
            },
            new PersonalityPreset
            {
                PresetName = "공격형",
                Tags = new List<string> { "공격적", "충동적", "경쟁적" },
                Values = new List<string> { "힘 중시", "명예 중시" }
            },
            new PersonalityPreset
            {
                PresetName = "관찰형",
                Tags = new List<string> { "호기심 많음", "사교적", "수다스러움" },
                Values = new List<string> { "정보 중시", "관계 중시" }
            },
            new PersonalityPreset
            {
                PresetName = "생존형",
                Tags = new List<string> { "계산적", "이기적", "현실적" },
                Values = new List<string> { "생존 우선", "효율 중시" }
            }
        };

        public static PersonalityPreset GetRandomPreset()
        {
            return Presets[Random.Range(0, Presets.Count)];
        }
    }
}
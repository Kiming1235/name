using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.NPC
{
    [Serializable]
    public class NpcData
    {
        public string Id;
        public string DisplayName;
        public int Age;
        public string Role;
        public PersonalityProfile Personality = new();
        public ValueProfile Values = new();
        public NeedProfile Needs = new();
        public EmotionProfile Emotion = new();
        public MemoryProfile Memory = new();
        public RelationshipProfile Relationships = new();
        public Vector2Int GridPosition;
        public NpcState CurrentState = NpcState.Idle;
        public string CurrentTargetId;
    }

    public enum NpcState
    {
        Idle,
        Moving,
        Eating,
        Sleeping,
        Gathering,
        Chopping,
        Mining,
        Carrying,
        Talking,
        Fleeing,
        Attacking
    }

    [Serializable]
    public class PersonalityProfile
    {
        public List<string> Tags = new();
    }

    [Serializable]
    public class ValueProfile
    {
        public List<string> Values = new();
    }

    [Serializable]
    public class NeedProfile
    {
        public float Hunger;
        public float Fatigue;
        public float Safety;
    }

    [Serializable]
    public class EmotionProfile
    {
        public float Fear;
        public float Anger;
        public float Joy;
    }

    [Serializable]
    public class MemoryProfile
    {
        public List<string> ShortTermMemories = new();
        public List<string> LongTermMemories = new();
    }

    [Serializable]
    public class RelationshipProfile
    {
        public List<RelationshipEntry> Entries = new();
    }

    [Serializable]
    public class RelationshipEntry
    {
        public string TargetNpcId;
        public float Trust;
        public float Affection;
        public float Fear;
        public float Hatred;
    }
}
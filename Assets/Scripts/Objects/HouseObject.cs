using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class HouseObject : WorldObject
    {
        [SerializeField] private int capacity = 2;
        [SerializeField] private List<string> residentNpcIds = new();
        [SerializeField] private Vector2Int doorPosition;
    }
}
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class GrassObject : WorldObject
    {
        [SerializeField] private int nutritionValue = 5;
        [SerializeField] private float regrowTime = 30f;
        [SerializeField] private bool isHarvestable = true;
    }
}
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class TreeObject : WorldObject
    {
        [SerializeField] private int woodAmount = 20;
        [SerializeField] private float growth = 1f;
        [SerializeField] private bool canBeChopped = true;
        [SerializeField] private bool isFruitTree;

        public override void TickObject()
        {
            growth = Mathf.Clamp01(growth + Time.deltaTime * 0.001f);
        }
    }
}
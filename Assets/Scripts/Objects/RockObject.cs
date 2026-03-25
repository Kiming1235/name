using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public class RockObject : WorldObject
    {
        [SerializeField] private int stoneAmount = 15;
        [SerializeField] private bool canBeMined = true;
    }
}
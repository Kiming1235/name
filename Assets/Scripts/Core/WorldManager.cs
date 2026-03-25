using System.Collections.Generic;
using UnityEngine;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.Core
{
    public class WorldManager : MonoBehaviour
    {
        private readonly List<WorldObject> registeredObjects = new();

        public void InitializeWorld()
        {
            registeredObjects.Clear();
        }

        public void RegisterObject(WorldObject worldObject)
        {
            if (worldObject == null || registeredObjects.Contains(worldObject))
            {
                return;
            }

            registeredObjects.Add(worldObject);
        }

        public void TickWorldObjects()
        {
            foreach (var worldObject in registeredObjects)
            {
                if (worldObject == null)
                {
                    continue;
                }

                worldObject.TickObject();
            }
        }
    }
}
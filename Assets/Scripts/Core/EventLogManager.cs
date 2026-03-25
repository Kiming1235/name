using System.Collections.Generic;
using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class EventLogManager : MonoBehaviour
    {
        private readonly Queue<string> pendingLogs = new();

        public void AddLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            pendingLogs.Enqueue(message);
        }

        public void FlushTickSummary()
        {
            while (pendingLogs.Count > 0)
            {
                Debug.Log(pendingLogs.Dequeue());
            }
        }
    }
}
using UnityEngine;

namespace QuarterVillageSim.Objects
{
    public abstract class WorldObject : MonoBehaviour
    {
        [SerializeField] protected string id;
        [SerializeField] protected string displayName;
        [SerializeField] protected Vector2Int gridPosition;
        [SerializeField] protected bool isBlocking;
        [SerializeField] protected bool isInteractable = true;
        [SerializeField] protected bool isDestroyed;
        [SerializeField] protected int hitPoints = 100;
        [SerializeField] protected string ownerNpcId;

        public string Id => id;
        public string DisplayName => displayName;
        public Vector2Int GridPosition => gridPosition;
        public bool IsBlocking => isBlocking;
        public bool IsInteractable => isInteractable;
        public bool IsDestroyed => isDestroyed;

        public virtual void TickObject()
        {
        }
    }
}
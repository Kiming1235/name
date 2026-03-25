using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private float tileWidth = 1f;
        [SerializeField] private float tileHeight = 0.5f;
        [SerializeField] private Vector3 worldOrigin = Vector3.zero;

        public Vector3 GridToWorld(Vector2Int gridPosition)
        {
            float worldX = (gridPosition.x - gridPosition.y) * (tileWidth * 0.5f);
            float worldY = (gridPosition.x + gridPosition.y) * (tileHeight * 0.5f);
            return worldOrigin + new Vector3(worldX, worldY, 0f);
        }

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            Vector3 local = worldPosition - worldOrigin;
            float gridX = (local.x / (tileWidth * 0.5f) + local.y / (tileHeight * 0.5f)) * 0.5f;
            float gridY = (local.y / (tileHeight * 0.5f) - local.x / (tileWidth * 0.5f)) * 0.5f;
            return new Vector2Int(Mathf.RoundToInt(gridX), Mathf.RoundToInt(gridY));
        }
    }
}
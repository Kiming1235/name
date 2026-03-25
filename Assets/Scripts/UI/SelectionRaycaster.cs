using UnityEngine;
using QuarterVillageSim.NPC;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.UI
{
    public class SelectionRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private NpcDetailPanel npcDetailPanel;
        [SerializeField] private ObjectDetailPanel objectDetailPanel;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySelect();
            }
        }

        private void TrySelect()
        {
            if (mainCamera == null)
            {
                return;
            }

            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point2D = new Vector2(worldPoint.x, worldPoint.y);
            Collider2D hit = Physics2D.OverlapPoint(point2D);
            if (hit == null)
            {
                return;
            }

            NpcController npc = hit.GetComponentInParent<NpcController>();
            if (npc != null)
            {
                npcDetailPanel?.ShowNpc(npc.Data);
                return;
            }

            WorldObject worldObject = hit.GetComponentInParent<WorldObject>();
            if (worldObject != null)
            {
                objectDetailPanel?.ShowObject(worldObject);
            }
        }
    }
}
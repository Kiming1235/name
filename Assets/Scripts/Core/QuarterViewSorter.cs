using UnityEngine;

namespace QuarterVillageSim.Core
{
    public class QuarterViewSorter : MonoBehaviour
    {
        [SerializeField] private int sortingOffset;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void LateUpdate()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            spriteRenderer.sortingOrder = -(int)(transform.position.y * 100f) + sortingOffset;
        }
    }
}
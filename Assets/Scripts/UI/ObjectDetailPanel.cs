using UnityEngine;
using TMPro;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.UI
{
    public class ObjectDetailPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text positionText;
        [SerializeField] private TMP_Text stateText;

        public void ShowObject(WorldObject worldObject)
        {
            if (worldObject == null)
            {
                return;
            }

            if (nameText != null) nameText.text = worldObject.DisplayName;
            if (positionText != null) positionText.text = worldObject.GridPosition.ToString();
            if (stateText != null) stateText.text = worldObject.IsDestroyed ? "파괴됨" : "정상";
        }
    }
}
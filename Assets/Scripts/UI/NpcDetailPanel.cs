using UnityEngine;
using TMPro;
using QuarterVillageSim.NPC;

namespace QuarterVillageSim.UI
{
    public class NpcDetailPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text stateText;
        [SerializeField] private TMP_Text personalityText;

        public void ShowNpc(NpcData data)
        {
            if (data == null)
            {
                return;
            }

            if (nameText != null) nameText.text = data.DisplayName;
            if (stateText != null) stateText.text = data.CurrentState.ToString();
            if (personalityText != null) personalityText.text = string.Join(", ", data.Personality.Tags);
        }
    }
}
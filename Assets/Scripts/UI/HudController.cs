using UnityEngine;
using TMPro;

namespace QuarterVillageSim.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text resourceText;

        public void SetTimeText(string value)
        {
            if (timeText != null) timeText.text = value;
        }

        public void SetSpeedText(string value)
        {
            if (speedText != null) speedText.text = value;
        }

        public void SetResourceText(string value)
        {
            if (resourceText != null) resourceText.text = value;
        }
    }
}
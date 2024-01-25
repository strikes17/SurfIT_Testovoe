using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIProductTimerWidget : GUIBaseWidget
    {
        [SerializeField] private Text _timerText;

        //Value comes in seconds
        public int Value
        {
            set
            {
                int hours = (int)Mathf.Floor(value / 3600f);
                int minutes = (int)(hours > 0 ? value / 60f % 60f : value / 60f);
                int seconds = (int)(minutes > 0 ? value % 60f : value);
                _timerText.text = $"{hours}h:{minutes}m:{seconds}s";
            }
        }
    }
}
using TMPro;
using UnityEngine;

namespace Shop.GUI
{
    public class GUIResourceWidget : GUIBaseWidget
    {
        [SerializeField] private TMP_Text _valueText;

        public int Value
        {
            set => _valueText.text = value.ToString();
        }
    }
}
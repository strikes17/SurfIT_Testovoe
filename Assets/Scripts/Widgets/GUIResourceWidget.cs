using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIResourceWidget : GUIBaseWidget
    {
        [SerializeField] private Text _valueText;

        public int Value
        {
            set => _valueText.text = value.ToString();
        }
    }
}
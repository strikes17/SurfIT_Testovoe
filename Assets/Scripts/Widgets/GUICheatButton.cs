using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUICheatButton : GUIBaseWidget
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        public Button.ButtonClickedEvent Clicked => _button.onClick;

        public string Text
        {
            set => _text.text = value;
        }
    }
}
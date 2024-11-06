using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIs.Popup
{
    internal class ConsolePopupUI : UIPopup
    {
        InputField input;
        private void Start()
        {
            input = GetComponentInChildren<InputField>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(input.text.Length > 0)
                {
                    GameManager.Instance.AcceptConsole(input.text);
                    OnCloseButton();
                }
            }
        }
    }
}

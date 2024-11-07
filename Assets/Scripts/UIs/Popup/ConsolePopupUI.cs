using UnityEngine;
using UnityEngine.UI;

public class ConsolePopupUI : UIPopup
{
    InputField input;
    private void Start()
    {
        input = GetComponentInChildren<InputField>();
        input.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length > 0)
            {
                GameManager.Instance.AcceptConsole(input.text);
                OnCloseButton();
            }
        }
    }
}

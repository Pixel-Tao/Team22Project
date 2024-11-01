using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : UIBase
{
    public virtual void OnCloseButton()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}

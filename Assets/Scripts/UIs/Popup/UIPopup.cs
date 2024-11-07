public class UIPopup : UIBase
{
    public virtual void OnCloseButton()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}

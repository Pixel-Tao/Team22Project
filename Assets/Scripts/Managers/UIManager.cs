using System;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, UIBase> uiBase = new Dictionary<string, UIBase>();

    private void Generate<T>()
    {

    }

    public void Open<T>() where T : UIBase
    {
        
    }

    public void Close<T>() where T : UIBase
    {

    }
}

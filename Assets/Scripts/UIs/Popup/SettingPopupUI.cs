using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopupUI : UIPopup
{
    public Toggle bgmToggle;
    public Toggle effectToggle;

    private void Start()
    {
        bgmToggle.SetIsOnWithoutNotify(SoundManager.Instance.IsBGMOn());
        effectToggle.SetIsOnWithoutNotify(SoundManager.Instance.IsEffectOn());
    }
    
    public void OnBGMToggle()
    {
        SoundManager.Instance.ToggleBGM(bgmToggle.isOn);
    }
    
    public void OnEffectToggle()
    {
        SoundManager.Instance.ToggleEffect(effectToggle.isOn);
    }
}
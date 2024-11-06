using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutPopupUI : UIPopup
{
    private Animator animator;
    private Image image;
    private Action callback;
    private bool isAutoClose;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        Debug.Log("FadeInOutPopupUI OnEnable");
        image.color = new Color(0, 0, 0, 1);
    }

    public void FadeIn(float delay = 1, bool isAutoClose = true, Action fadedCallback = null)
    {
        this.callback = fadedCallback;
        this.isAutoClose = isAutoClose;
        animator.Play("FadeIn");
        Invoke("OnFaded", delay);
    }

    public void FadeOut(float delay = 1, bool isAutoClose = true, Action fadedCallback = null)
    {
        this.callback = fadedCallback;
        this.isAutoClose = isAutoClose;
        animator.Play("FadeOut");
        Invoke("OnFaded", delay);
    }

    private void OnFaded()
    {
        callback?.Invoke();
        callback = null;
        if (isAutoClose) OnCloseButton();
    }
}

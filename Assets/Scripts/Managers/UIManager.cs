using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Diagnostics;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    int order = -20;

    Stack<UIPopup> popupStack = new Stack<UIPopup>();
    Dictionary<string, UIPopup> popupDict = new Dictionary<string, UIPopup>();

    public bool IsPopupOpeing => popupStack.Count > 0;

    public UIScene SceneUI { get; private set; }
    private GameObject root;
    public GameObject Root
    {
        get
        {
            if (root == null)
            {
                root = GameObject.Find("UI_Root");
                if (root == null)
                    root = new GameObject { name = "UI_Root" };
            }
            return root;
        }
    }

    public override void Init()
    {
        base.Init();
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            obj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = ResourceManager.Instance.Load<GameObject>($"Prefabs/UI/SubItem/{name}");

        GameObject go = ResourceManager.Instance.Instantiate(prefab);
        if (parent != null)
            go.transform.SetParent(parent);

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Scene/{name}");
        T sceneUI = Utils.GetOrAddComponent<T>(go);
        SceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        if (!popupDict.TryGetValue(name, out UIPopup popup))
        {
            GameObject prefab = ResourceManager.Instance.Load<GameObject>($"Prefabs/UI/Popup/{name}");

            GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");
            popup = Utils.GetOrAddComponent<T>(go);
            popupDict.Add(name, popup);

            if (parent != null)
                go.transform.SetParent(parent);
            else if (SceneUI != null)
                go.transform.SetParent(SceneUI.transform);
            else
                go.transform.SetParent(Root.transform);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = prefab.transform.position;
        }
        popup.gameObject.SetActive(true);
        popupStack.Push(popup);

        return popup as T;
    }

    public T FindPopup<T>() where T : UIPopup
    {
        return popupStack.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
    }

    public T PeekPopupUI<T>() where T : UIPopup
    {
        if (popupStack.Count == 0)
            return null;

        return popupStack.Peek() as T;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (popupStack.Count == 0)
            return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();
        popup.gameObject.SetActive(false);
        popup = null;
        order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }

    public void Prompt(string text = null)
    {
        SceneUI.Prompt(text);
    }

    public void FadeIn(float delay = 1, bool isAutoClose = true, Action fadedCallback = null)
    {
        var fade = ShowPopupUI<FadeInOutPopupUI>();
        fade.FadeIn(delay, isAutoClose, fadedCallback);
    }
    public void FadeOut(float delay = 1, bool isAutoClose = true, Action fadedCallback = null)
    {
        var fade = ShowPopupUI<FadeInOutPopupUI>();
        fade.FadeOut(delay, isAutoClose, fadedCallback);
    }
    public void FadeClose()
    {
        var fade = FindPopup<FadeInOutPopupUI>();
        if (fade != null)
            fade.FadeOut();
    }
}

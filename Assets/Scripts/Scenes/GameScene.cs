using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : SceneBase
{
    public int poolDefaultCapacity = 20;
    public int poolMaxSize = 100;

    private void CheckEventSystem()
    {
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            obj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }
    }
    private void Init()
    {
        CharacterManager.Instance.Init();
        GameManager.Instance.Init();
        PoolManager.Instance.Init(poolDefaultCapacity, poolMaxSize);
        SoundManager.Instance.Init();

    }
    private void GameSetting()
    {
        CharacterManager.Instance.LoadPlayer(Defines.JobType.Knight);
        ResourceManager.Instance.Instantiate("WorldMap");
        SoundManager.Instance.SetBackGroundMusic("BGM");
    }
    private void UISetting()
    {
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
    }
    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        CheckEventSystem();
        FadeInOutPopupUI fade = UIManager.Instance.ShowPopupUI<FadeInOutPopupUI>();
        Init();
        GameSetting();
        UISetting();
        fade?.FadeIn(2);
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");

    }
}
using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameScene : SceneBase
{
    public int poolDefaultCapacity = 20;
    public int poolMaxSize = 100;

    private void Init()
    {
        UIManager.Instance.Init();
        CharacterManager.Instance.Init();
        GameManager.Instance.Init();
        PoolManager.Instance.Init(poolDefaultCapacity, poolMaxSize);
        SoundManager.Instance.Init();

    }
    private void GameSetting()
    {
        CharacterManager.Instance.LoadPlayer(Defines.JobType.Knight);
        GameObject go = ResourceManager.Instance.Instantiate("WorldMap");
        NavMeshSurface navMeshSurface = go.GetComponent<NavMeshSurface>();
        ResourceManager.Instance.Instantiate("DayAndNight");

        //navMeshSurface.BuildNavMesh();
        //SoundManager.Instance.SetBackGroundMusic("BGM");
    }
    private void UISetting()
    {
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
    }
    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        FadeInOutPopupUI fade = UIManager.Instance.ShowPopupUI<FadeInOutPopupUI>();
        Init();
        GameSetting();
        UISetting();
        fade?.FadeIn();
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");

    }
}
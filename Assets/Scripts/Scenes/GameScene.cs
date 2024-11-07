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
        GameObject mapGameObject = ResourceManager.Instance.Instantiate("WorldMap");
        NavMeshBaking(mapGameObject);
        ResourceManager.Instance.Instantiate("Spawner/Spawners");
        ResourceManager.Instance.Instantiate("DayAndNight");
        SoundManager.Instance.SetBackGroundMusic("BGM");
    }
    private void UISetting()
    {
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
    }

    private void NavMeshBaking(GameObject map)
    {
        NavMeshSurface navMeshSurface = map.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        NavMeshData navMeshData = navMeshSurface.navMeshData;
        navMeshData.position -= new Vector3(0, 0.07f, 0);
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
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
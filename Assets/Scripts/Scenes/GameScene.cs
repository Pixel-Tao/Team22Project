using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        Debug.Log("GameScene OnSceneLoad");
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("GameScene OnSceneUnloaded");
    }
}
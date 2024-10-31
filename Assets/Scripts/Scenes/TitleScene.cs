using UnityEngine;

public class TitleScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        Debug.Log("TitleScene OnSceneLoad");
    }

    protected override void OnSceneUnloaded()
    {
        Debug.Log("TitleScene OnSceneUnloaded");
    }
}
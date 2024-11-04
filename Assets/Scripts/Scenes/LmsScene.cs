using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LmsScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        CharacterManager.Instance.LoadPlayer(Defines.JobType.Babarian);
        UIManager.Instance.ShowSceneUI<GameSceneUI>();
    }

    protected override void OnSceneUnloaded()
    {
    }
}

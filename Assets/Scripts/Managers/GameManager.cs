using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsBuildMode { get; private set; } = false;

    public void ToggleBuildMode()
    {
        IsBuildMode = !IsBuildMode;
        if (IsBuildMode)
        {
            // 건설모드일때 카메라 시점 변경
            CharacterManager.Instance.Player.BuildMode();
        }
        else
        {
            // 건설모드가 아닐때 카메라 시점 변경
            CharacterManager.Instance.Player.NormalMode();
        }
    }

}

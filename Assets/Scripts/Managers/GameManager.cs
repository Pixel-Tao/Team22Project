using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsBuildMode { get; private set; } = false;

    public int WoodCount { get; private set; } = 0;
    public int OreCount { get; private set; } = 0;
    public int PeopleCount { get; private set; } = 0;
    public int MaxPeopleCount { get; private set; } = 0;
    public int FoodCount { get; private set; } = 0;

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

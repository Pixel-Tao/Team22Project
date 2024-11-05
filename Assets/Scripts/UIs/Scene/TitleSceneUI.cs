
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : UIScene
{
    // 타이틀씬에서 사용할 고정 UI

    public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionButton()
    {
        // TODO : 설정
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}

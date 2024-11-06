
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : UIScene
{
    // 타이틀씬에서 사용할 고정 UI

    public void StartButton()
    {
        UIManager.Instance.FadeOut(2, false,() =>
        {
            SceneManager.LoadScene("GameScene");
            UIManager.Instance.SingletonDestroy();
            UIManager.Instance.FadeClose();
        });
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

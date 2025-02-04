
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : UIScene
{
    // 타이틀씬에서 사용할 고정 UI

    public void StartButton()
    {
        UIManager.Instance.FadeOut(2, false,() =>
        {
            UIManager.Instance.SingletonDestroy();
            GameManager.Instance.SingletonDestroy();

            SceneManager.LoadScene("GameScene");
            UIManager.Instance.FadeClose();
        });
    }

    public void OptionButton()
    {
        // TODO : 설정
        UIManager.Instance.ShowPopupUI<SettingPopupUI>();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    BuildingCondition condition;
    private void Awake()
    {
        GameManager.Instance.AddGoal(this);
        condition = gameObject.GetComponent<BuildingCondition>();
    }

    private void Update()
    {
        if(condition.CurHealth <= 0f)
        {
            UIManager.Instance.SingletonDestroy();
            SoundManager.Instance.SingletonDestroy();   
            SceneManager.LoadScene("TitleScene");
        }
    }
}

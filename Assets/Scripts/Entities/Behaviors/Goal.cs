using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    BuildingCondition condition;
    private void Awake()
    {
        GameManager.Instance.AddGoal(this);
    }
}

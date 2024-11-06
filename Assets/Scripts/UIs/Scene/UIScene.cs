using TMPro;
using UnityEngine;

public class UIScene : UIBase
{
    // 씬에서 고정적으로 사용할 UI 들이 상속받습니다.

    private TextMeshProUGUI promptText;
    private Transform promptTransform;
    private TextMeshProUGUI systemMessageText;
    private Transform systemMessageTransform;
    [SerializeField] private TextMeshProUGUI modeText;

    [SerializeField] private float systemMessageDuration = 5f;
    private float systemMessageTimer;

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        promptTransform = rect.Find("PromptText");
        promptText = promptTransform?.GetComponentInChildren<TextMeshProUGUI>();
        systemMessageTransform = rect.Find("SystemMessageText");
        systemMessageText = systemMessageTransform?.GetComponentInChildren<TextMeshProUGUI>();

        Prompt(null);
        SystemMessage(null);
        ModeChange(GameManager.Instance.IsBuildMode);
    }

    private void Update()
    {
        SystemMessageUpdate();
    }

    private void SystemMessageUpdate()
    {
        if (systemMessageText == null) return;
        if (systemMessageTransform.gameObject.activeInHierarchy)
        {
            systemMessageTimer += Time.deltaTime;
            if (systemMessageTimer >= systemMessageDuration)
            {
                systemMessageTimer = 0;
                SystemMessage(null);
            }
        }
    }

    public void Prompt(string text)
    {
        if (promptText == null)
            return;

        if (string.IsNullOrWhiteSpace(text))
        {
            promptText.text = string.Empty;
            promptTransform.gameObject.SetActive(false);
        }
        else
        {
            if (!promptTransform.gameObject.activeInHierarchy)
                promptTransform.gameObject.SetActive(true);

            promptText.text = text;
        }
    }

    public void SystemMessage(string text)
    {
        if (systemMessageText == null)
            return;

        if (string.IsNullOrWhiteSpace(text))
        {
            systemMessageText.text = string.Empty;
            systemMessageTransform.gameObject.SetActive(false);
        }
        else
        {
            if (!systemMessageTransform.gameObject.activeInHierarchy)
                systemMessageTransform.gameObject.SetActive(true);

            systemMessageText.text = text;
            systemMessageTimer = 0;
        }
    }

    public void ModeChange(bool isBuildMode)
    {
        if (modeText == null) return;
        string text = "(Q) ";
        text += isBuildMode ? "건설 모드" : "일반 모드";
        modeText.text = text;
    }
}

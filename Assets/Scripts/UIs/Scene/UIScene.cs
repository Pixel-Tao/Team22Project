using TMPro;
using UnityEngine;

public class UIScene : UIBase
{
    // 씬에서 고정적으로 사용할 UI 들이 상속받습니다.

    private TextMeshProUGUI promptText;
    private Transform promptTransform;

    [SerializeField] private float promptDuration = 3f;
    private float promptTimer;

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        promptTransform = rect.Find("PromptText");
        promptText = promptTransform?.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        Prompt();
    }

    private void Update()
    {
        PromptUpdate();
    }

    private void PromptUpdate()
    {
        if (promptText == null) return;
        if (promptTransform.gameObject.activeInHierarchy)
        {
            promptTimer += Time.deltaTime;
            if (promptTimer >= promptDuration)
            {
                promptTimer = 0;
                Prompt();
            }
        }
    }

    public void Prompt(string text = null)
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
            promptTimer = 0;
        }
    }
}

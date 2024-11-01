using UnityEngine;
using UnityEngine.UI;

public class ConditionSlot : MonoBehaviour
{
    public Image bar;
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;

    private void Start()
    {
        currentValue = startValue;
    }

    private void Update()
    {
        bar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return currentValue / maxValue;
    }

    public void Add(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
    }

    public void Subtract(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
    }
}

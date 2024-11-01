using UnityEngine;
using UnityEngine.UI;

public class ConditionSlot : MonoBehaviour
{
    public Image bar;
    private float currentValue;
    public float maxValue;
    
    public void ValueChanged(float value, float maxValue)
    {
        currentValue = value;
        maxValue = value;
        bar.fillAmount = value / maxValue;
    }
}

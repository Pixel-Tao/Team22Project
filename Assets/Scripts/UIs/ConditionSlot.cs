using UnityEngine;
using UnityEngine.UI;

public class ConditionSlot : MonoBehaviour
{
    public Image bar;
    private float currentValue;
    public float maxValue;
    
    public void ValueChanged(float value, float maxValue)
    {
        this.currentValue = value;
        this.maxValue = maxValue;
        bar.fillAmount = value / maxValue;
    }
}

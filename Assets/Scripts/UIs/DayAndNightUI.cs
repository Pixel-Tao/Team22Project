using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightUI : MonoBehaviour
{
    [SerializeField] private GameObject dayPanel;
    [SerializeField] private GameObject nightPanel;

    public void SetDay()
    {
        dayPanel.SetActive(true);
        nightPanel.SetActive(false);
    }

    public void SetNight()
    {
        dayPanel.SetActive(false);
        nightPanel.SetActive(true);
    }
}

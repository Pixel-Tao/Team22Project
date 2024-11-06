using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float dayLength;
    public float nightLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    private float dayTime;
    private float nightTime;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private void Start()
    {
        //timeRate = 1.0f / fullDayLength;
        dayTime = 1.0f / dayLength;
        nightTime = 1.0f / nightLength;
        time = startTime;
    }

    private void Update()
    {
        if (time >= 0.25f && time <= 0.75f)
        {
            if (dayLength > 0)
            {
                time = (time + dayTime * Time.deltaTime) % 1.0f;
            }
            else
            {
                time = (time + nightTime * Time.deltaTime) % 1.0f;
            }
        }
        else
        {
            if (nightLength > 0)
            {
                time = (time + nightTime * Time.deltaTime) % 1.0f;
            }
            else
            {
                time = (time + dayTime * Time.deltaTime) % 1.0f;
            }
        }
        //time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
            go.SetActive(false);
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
            go.SetActive(true);
    }
}
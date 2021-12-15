using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;


    [Header("Sun")]
    public Light sun;
    public Gradient sunColour;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColour;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultiplier;

    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }
    void Update()
    {
        // Incrementing time
        time += timeRate * Time.deltaTime;

        if (time >= 1.0f)
        {
            time = 0.0f;
        }

        // Light rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        // Change colours as the day goes on 
        sun.color = sunColour.Evaluate(time);
        moon.color = moonColour.Evaluate(time);

        // Disable sun/moon when necessary
        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
        }
        else if (sun.intensity > 0 && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(true);
        }
        
        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(false);
        }
        else if (moon.intensity > 0 && moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(true);
        }

        // Lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);

    
    }
}

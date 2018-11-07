using UnityEngine;
using Random = System.Random;

public class BrokenLight : MonoBehaviour
{
    [Range(0f,1f)]
    public float percentageOn = .5f;

    [Range(0f, 1f)]
    public float percentageOnWeak = .5f;

    public float interval = 2f;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private Light light;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    private Random rdm = new Random();
    private float intervalTime;

    // Use this for initialization
    void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        intervalTime += Time.deltaTime;

        if (intervalTime > interval)
        {
            float r = (float) rdm.Next(0, 100) / 100;

            light.enabled = !(r < 1 - percentageOn);

            if (r < percentageOnWeak / (1 - percentageOn))
            {
                light.intensity = 2;
            }
            else
            {
                light.intensity = 5;
            }

            intervalTime = 0;
        }
    }
}

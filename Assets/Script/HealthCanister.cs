using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCanister : MonoBehaviour
{
    public float speed = 5f;
    public Material[] Materials;

    private float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        gameObject.transform.position += new Vector3(Time.deltaTime * speed, 0, 0);

        if (time < 1)
            gameObject.GetComponent<Renderer>().material = Materials[0];
        else if (time > 1)
        {
            gameObject.GetComponent<Renderer>().material = Materials[1];
        }
        else if (time > 2)
            time = 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Danger : MonoBehaviour
{
    public Material Material;
    public GameObject SpawnControllerGameObject;
	public float speed = 1f;
    public float destructAt = 10f;
    public int   wave = 0;

    private Random rdm = new Random();

    void Start()
    {
        
    }

    void Update()
    {
        Material.color = new Color((float)rdm.Next(0, 1000) / 1000, (float)rdm.Next(0, 1000) / 1000, (float)rdm.Next(0, 1000) / 1000);
        gameObject.GetComponent<Renderer>().material = Material;

        gameObject.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

        gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (gameObject.transform.localPosition.x >= destructAt)
        {
            try
            {
                SpawnControllerGameObject.GetComponent<SpawnerController>().waveCleared = wave;
            }
            catch{}

            Destroy(gameObject);
        }
    }
}

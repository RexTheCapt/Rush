using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public GameObject SpawnControllerGameObject;
	public float speed = 1f;
    public float destructAt = 10f;
    public int   wave = 0;

    void Start()
    {

    }

    void Update()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject[] CountGameObjects;
    public GameObject[] CubesGameObjects;
    public GameObject SpawnControllerGameObject;
    public GameObject PlayerGameObject;
    public Material RedMaterial;
    public Material GreenMaterial;

    private PlayerControll Player
    {
        get { return PlayerGameObject.GetComponent<PlayerControll>(); }
    }

    private SpawnerController Controller
    {
        get { return SpawnControllerGameObject.GetComponent<SpawnerController>(); }
    }

    private float timer;


    // Use this for initialization
    void Start()
    {
        foreach (var countGameObject in CountGameObjects)
        {
            countGameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4)
        {
            Controller.SpawnTimer = 50;
            Destroy(gameObject);
        }
        else if (timer > 3)
        {
            CountGameObjects[2].SetActive(true);
            foreach (GameObject o in CubesGameObjects)
            {
                o.GetComponent<Renderer>().material = GreenMaterial;
            }
        }
        else if (timer > 2)
        {
            CountGameObjects[1].SetActive(true);
        }
        else if (timer > 1)
        {
            CountGameObjects[0].SetActive(true);
            foreach (GameObject o in CubesGameObjects)
            {
                o.GetComponent<Renderer>().material = RedMaterial;
            }
        }
    }

    void FixedUpdate()
    {
        Controller.SpawnTimer = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject DangerGameObject;
    public GameObject Spawner0GameObject;
    public float SpawnTimer
    {
        get { return Spawner0GameObject.GetComponent<SpawnerController>().spawnCurrentRate; }
        set { Spawner0GameObject.GetComponent<SpawnerController>().spawnCurrentRate = value; }
    }
    public float lastSpawnTime;

	// Use this for initialization
	void Start () {
		Spawner0GameObject.GetComponent<SpawnerController>().RegisterSpawner(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    lastSpawnTime += Time.deltaTime;
	}

    public GameObject SpawnDanger(int wave)
    {
        GameObject spawnGameObject = Instantiate(DangerGameObject);

        spawnGameObject.transform.position = gameObject.transform.position;
        spawnGameObject.gameObject.GetComponent<Danger>().SpawnControllerGameObject = Spawner0GameObject;
        spawnGameObject.gameObject.GetComponent<Danger>().wave = wave;

        Spawner0GameObject.GetComponent<SpawnerController>().AddDanger(spawnGameObject);

        lastSpawnTime = 0;

        return DangerGameObject;
    }
}

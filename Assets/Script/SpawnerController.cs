using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SpawnerController : MonoBehaviour {

    private System.Random rdm = new System.Random();
    private List<GameObject> dangerObjects = new List<GameObject>();
    private List<GameObject> spawnerGameObjects = new List<GameObject>();
    private AudioSource Audio
    {
        get { return PlayerGameObject.GetComponent<AudioSource>(); }
    }

    public bool playerIsDead = false;
    public AudioClip WaveClearedAudioClip;

    [Header("Game Objects")]
    public GameObject PlayerGameObject;
    public GameObject HighScoreGameObject;
    public GameObject CurrentScoreGameObject;

    [Header("Spawn settings")]
    public float SpawnTimer;
    public float spawnBaseRate = 5f;
    public float spawnRateModifier = 1000f;
    public float spawnCurrentRate;
    public float spawnMinRate = 0.5f;
    public int maxActiveSpawners = 7;
    public int activeSpawners = 1;

    [Header("Danger settings")]
    public float dangerBaseSpeed = 1f;
    public float dangerSpeedModifier = 100f;
    public float dangerCurrentSpeed;
    public float dangerDestructAt = 10f;

    [Header("Waves")]
    public bool waveReset = false;
    public int waveSpawned;
    public int waveCleared;
    private int _waveClearedPrev;
    public int waveLock;

    [Header("Overrides")]
    public bool OverrideScore = false;
    public int OverrideScoreSet = 0;

    // Use this for initialization
	void Start ()
	{
	    HighScoreGameObject.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("HighScore").ToString("00");
	    CurrentScoreGameObject.GetComponent<TextMeshPro>().text = "00";

	    if (OverrideScore)
	    {
            PlayerPrefs.SetInt("HighScore", OverrideScoreSet);
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (waveReset)
	    {
	        waveCleared = 0;
            waveReset = false;
	        waveSpawned = 0;

	        foreach (var o in dangerObjects)
	        {
	            Destroy(o);
	        }
	    }

	    if (waveLock > -1)
	    {
	        waveCleared = waveLock;
	        waveSpawned = waveLock;
	    }

	    if (!playerIsDead)
	    {
            // Time since last spawned dangers
            SpawnTimer += Time.deltaTime;
            // The current speed of the danger blocks
            dangerCurrentSpeed = dangerBaseSpeed * (((float)waveCleared / dangerSpeedModifier) + 1);
            // The current spawn rate of the danger blocks
            spawnCurrentRate = spawnBaseRate - ((float)waveCleared / spawnRateModifier);
            // Set current active spawners
            activeSpawners = (waveCleared / 10) + 1;

            #region Update text display

	        if (_waveClearedPrev < waveCleared)
	        {
	            Audio.clip = WaveClearedAudioClip;
                Audio.Play();
	        }

	        CurrentScoreGameObject.GetComponent<TextMeshPro>().text = waveCleared.ToString("00");
	        _waveClearedPrev = waveCleared;

            #endregion

            if (activeSpawners > maxActiveSpawners)
                activeSpawners = maxActiveSpawners;

            // Make sure the spawn rate do not get too low
            if (spawnCurrentRate < spawnMinRate)
                spawnCurrentRate = spawnMinRate;

            // Spawn the dangers
            if (SpawnTimer >= spawnCurrentRate)
            {
                // Count how many waves has been created
                waveSpawned++;

                List<GameObject> templist = new List<GameObject>(spawnerGameObjects);
                
                foreach (GameObject o in templist)
                {
                    if (PlayerIsInLane(o))
                    {
                        SpawnDanger(o);
                        templist.Remove(o);
                        break;
                    }
                }

                int spawned = 1;

                while (spawned < activeSpawners)
                {
                    int sel = rdm.Next(0, templist.Count);

                    if (SpawnDanger(templist[sel]) != null)
                    {
                        templist.RemoveAt(sel);
                        spawned++;
                    }
                }

                // Reset spawn timer to 0
                SpawnTimer = 0f;
            }

            for (int i = dangerObjects.Count - 1; i > -1; i--)
            {
                if (dangerObjects[i] != null)
                {
                    dangerObjects[i].gameObject.GetComponent<Danger>().speed = dangerCurrentSpeed;
                    dangerObjects[i].gameObject.GetComponent<Danger>().destructAt = dangerDestructAt;
                }
                else
                {
                    dangerObjects.RemoveAt(i);
                }
            }
        }
	    else
	    {
	        foreach (GameObject o in dangerObjects)
            {
                o.GetComponent<Danger>().speed = 0f;
                dangerCurrentSpeed = 0f;
            }
        }
	}

    private bool PlayerIsInLane(GameObject spawnGameObject)
    {
        if (PlayerGameObject.transform.position.z == spawnGameObject.transform.position.z)
            return true;
        return false;
    }

    private GameObject SpawnDanger(GameObject spawnerGameObject)
    {
        // Spawn a new danger
        GameObject spawnGameObject = spawnerGameObject.GetComponentInParent<Spawner>().SpawnDanger(waveSpawned);
        return spawnGameObject;
    }

    public void AddDanger(GameObject danger)
    {
        dangerObjects.Add(danger);
    }

    public void RegisterSpawner(GameObject spawnerGameObject)
    {
        spawnerGameObjects.Add(spawnerGameObject);
    }

    public void PlayerDead()
    {
        playerIsDead = true;

        if (PlayerPrefs.GetInt("HighScore") < waveCleared)
        {
            PlayerPrefs.SetInt("HighScore", waveCleared);
            PlayerPrefs.Save();
        }
    }
}

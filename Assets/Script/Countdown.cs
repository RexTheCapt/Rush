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

    [Header("Sounds")]
    public AudioClip ShortClip;
    public AudioClip LongClip;

    private PlayerControll Player
    {
        get { return PlayerGameObject.GetComponent<PlayerControll>(); }
    }

    private SpawnerController Controller
    {
        get { return SpawnControllerGameObject.GetComponent<SpawnerController>(); }
    }

    private float timer;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private AudioSource Audio
    {
        get { return gameObject.GetComponent<AudioSource>(); }
    }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private readonly bool[] stagePlayBools = new[] {false, false, false};


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

            if (!stagePlayBools[2])
            {
                PlaySound(true);
                stagePlayBools[2] = true;
            }
        }
        else if (timer > 2)
        {
            CountGameObjects[1].SetActive(true);

            if (!stagePlayBools[1])
            {
                PlaySound();
                stagePlayBools[1] = true;
            }
        }
        else if (timer > 1)
        {
            CountGameObjects[0].SetActive(true);

            foreach (GameObject o in CubesGameObjects)
            {
                o.GetComponent<Renderer>().material = RedMaterial;
            }

            if (!stagePlayBools[0])
            {
                PlaySound();
                stagePlayBools[0] = true;
            }
        }
    }

    void FixedUpdate()
    {
        Controller.SpawnTimer = 0;
    }

    void PlaySound(bool longSound = false)
    {
        if (longSound)
        {
            Audio.clip = LongClip;
        }
        else
        {
            Audio.clip = ShortClip;
        }

        Audio.Play();
    }
}

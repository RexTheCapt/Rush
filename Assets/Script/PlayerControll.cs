using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public bool invincible = false;
    public int Health
    {
        get { return _health; }
        set
        {
            if (_health > value)
            {
                damageFlash = true;
            }

            _health = value;
        }
    }
    [SerializeField]
    private int _health = 3;

    [Header("Game objects")]
    public GameObject SpawnControllerGameObject;
    public GameObject HealthDisplayGameObject;

    [Header("Score table options")]
    public GameObject ScoreTableGameObject;
    public bool enableScoreTable = true;

    [Header("Damage light effect")]
    public GameObject DamageLightGameObject;
    public bool damageFlash = false;
    public float damageTimer;
    public float damageTimerMax = 0.5f;

    [Header("Player lights")]
    public Light[] PlayerLight;

    private bool PlayerIsDead
    {
        get { return SpawnControllerGameObject.GetComponent<SpawnerController>().playerIsDead; }
    }

    // Use this for initialization
    void Start()
    {
        ScoreTableGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthDisplayGameObject.GetComponent<TextMeshPro>().text = Health.ToString("00");

        if (Health < 0)
            Health = 0;

        if (damageFlash)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer > damageTimerMax)
            {
                damageFlash = false;
                damageTimer = 0;
                DamageLightGameObject.SetActive(false);
            }
            else
            {
                DamageLightGameObject.SetActive(true);
            }
        }

        if (!PlayerIsDead)
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.A))
                move.z = -1;
            if (Input.GetKeyDown(KeyCode.D))
                move.z = 1;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, -3);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, -2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, -1);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, 0);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, 1);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, 2);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, 3);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                gameObject.transform.localPosition = new Vector3(47.1f, 1, 4);

            gameObject.transform.position += move;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x - Time.deltaTime,
                gameObject.transform.localScale.y - Time.deltaTime,
                gameObject.transform.localScale.z - Time.deltaTime);

            if (gameObject.transform.localScale.z < 0)
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                ScoreTableGameObject.SetActive(enableScoreTable);
            }

            foreach (Light l in PlayerLight)
            {
                l.intensity -= Time.deltaTime;

                if (l.intensity < 0)
                    l.intensity = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "Danger" || collision.gameObject.tag == "Wall")
            {
                Health--;

                if (collision.gameObject.tag == "Wall")
                {
                    gameObject.transform.localPosition = new Vector3(
                        transform.localPosition.x,
                        transform.localPosition.y,
                        0);
                }
            }
        }

        if (Health <= 0)
            SpawnControllerGameObject.GetComponent<SpawnerController>().PlayerDead();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTableButton : MonoBehaviour
{
    public GameObject ScoreTableGameObject;
    public float hoverTimer;
    public float clickTimer;

    #region privates

    private ScoreTableSettings ScoreSettings
    {
        get { return ScoreTableGameObject.GetComponent<ScoreTableSettings>(); }
    }
    private float HoverOffAt
    {
        get { return ScoreSettings.hoverOffAt; }
    }
    private float ClickOffAt
    {
        get { return ScoreSettings.clickOffAt; }
    }
    #region Materials

    private Material NormalMaterial
    {
        get { return ScoreSettings.NormalMaterial; }
    }
    private Material HoverMaterial
    {
        get { return ScoreSettings.HoverMaterial; }
    }
    private Material ClickMaterial
    {
        get { return ScoreSettings.ClickMaterial; }
    }

    #endregion

    #endregion

    // Use this for initialization
    void Start()
    {
        clickTimer = hoverTimer = 999f;
    }

    // Update is called once per frame
    void Update()
    {
        hoverTimer += Time.deltaTime;
        clickTimer += Time.deltaTime;

        if(hoverTimer > HoverOffAt)
        {
            gameObject.GetComponent<Renderer>().material = NormalMaterial;
        }
        else if (hoverTimer < HoverOffAt)
        {
            gameObject.GetComponent<Renderer>().material = HoverMaterial;
        }

        if (clickTimer < ClickOffAt)
        {
            gameObject.GetComponent<Renderer>().material = ClickMaterial;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MouseOver()
    {
        hoverTimer = 0;
    }

    public void Click()
    {
        clickTimer = 0;
    }
}

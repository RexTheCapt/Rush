using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTableClick : MonoBehaviour
{
    public Camera MainCamera;

    private Ray ray;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ScoreTableButton button = hit.transform.gameObject.GetComponent<ScoreTableButton>();
            if (button != null)
            {
                button.MouseOver();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                button.Click();
            }
        }
    }
}

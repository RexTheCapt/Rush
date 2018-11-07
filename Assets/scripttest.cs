using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scripttest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(Time.deltaTime, 0, 0);
    }
}

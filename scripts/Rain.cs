using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    GameObject cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //float t = transform.position.z;
        //Vector3 tmp = cam.transform.position;
        //tmp.z = t;
        //transform.position = tmp;
        //GetComponent<ParticleSystem>().po
    }
}

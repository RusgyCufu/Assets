using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float cameraShake = 0f;
    private CameraFollow cam;
    public float hp = 1;

    public float laserDmg = .1f;
    public GameObject[] resetRL;
    public int p1;
    public int p2;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    public void StartFight()
    {
        GetComponent<Animator>().SetTrigger("DoStart");
        GetComponent<Animator>().SetInteger("Stage", 1);
        GetComponent<dialogNPC>().startDialogPointer = p1;
    }
    public void StartStage2()
    {
        GetComponent<Animator>().SetTrigger("DoStage2");
        GetComponent<dialogNPC>().startDialogPointer = p1;
    }
    public void MainDed()
    {
        //reset to srage 1
        foreach(var i in resetRL)
        {
            i.GetComponent<RocketLauncher>().ResetR();
        }
        GetComponent<Animator>().SetBool("DoReset", true);
        if(GetComponent<Animator>().GetInteger("Stage") == 1)
        {
            hp = 1f;
        }
        
    }
    void Update()
    {
        if(GetComponent<Animator>().GetInteger("Stage") == 1 && hp < 0.77f)
        {
            GetComponent<Animator>().SetInteger("Stage", 2);
            GetComponent<Animator>().SetTrigger("FinStage");
            GetComponent<dialogNPC>().startDialogPointer = p2;
        }
        if(cameraShake != 0f)
        {
            cam.shake = true;
            cam.shakeRange = cameraShake;
        }
        else
        {
            cam.shake = false;
        }


    }
}

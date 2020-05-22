using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public float launchDelay = 1f;
    private float launchTimer = 0f;
    public GameObject target;
    public float dmg = 0.2f;
    public bool infRockets = true;
    public int maxRockets = 6;
    private int rocketsLeft;
    public TMPro.TextMeshProUGUI text;

    public void ResetR()
    {
        rocketsLeft = maxRockets;
    }

    private void Start()
    {
        ResetR();
    }
    public void SpawnRocket()
    {
        if (infRockets == false && rocketsLeft <= 0) return;

        GameObject rocket = Instantiate(rocketPrefab) as GameObject;
        rocket.transform.position = transform.position;
        rocket.transform.rotation = transform.rotation;
        rocket.GetComponent<Bomb>().damage = dmg;
        rocket.GetComponent<Bomb>().target = target;
        if(infRockets == false)
        {
            rocketsLeft -= 1;
        }
    }

    void Update()
    {
        launchTimer += Time.deltaTime;

        if(launchTimer >= launchDelay)
        {
            SpawnRocket();
            launchTimer = 0f;
        }

        if (infRockets == false)
        {
            text.text = rocketsLeft.ToString() + "/" + maxRockets.ToString();
        }
        else
        {
            text.text = "";
        }
    }
}

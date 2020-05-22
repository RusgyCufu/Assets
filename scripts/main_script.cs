﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class main_script : MonoBehaviour
{
    [SerializeField] public bool debug = true;
    [SerializeField] public bool BlockInput = false;

    [Header("skins")]
    [SerializeField] public Sprite[] skins;
    [SerializeField] public int skinID;
    [SerializeField] public string[] skinNamesEN;
    [SerializeField] public string[] skinNamesRU;


    [Header("Other")]
    [SerializeField] public Transform player;
    [HideInInspector] public GameObject resp;
    [SerializeField] public GameObject[] checkpoints;
    [SerializeField] public int coins;
    [SerializeField] UnityEvent deathEvent;

    [Header("Rain")]
        public float hp = 1f;
        public bool doRainDamage = false;
        public float rainDamage = 0.1f;
        public bool healByTime = true;
        public float healValue = 0.1f;


    private GameObject restartScreen;

    void RewriteCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void AddCoin()
    {
        coins += 1;
        RewriteCoins();
    }
    public void RemoveCoin()
    {
        coins -= 1;
        RewriteCoins();
    }
    public void respawn(GameObject respawnPoint)
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.parent = null;
        this.transform.position = respawnPoint.transform.position;
        GameObject.FindGameObjectWithTag("Phantom").GetComponent<Phantom>().ResetPos();

        if (deathEvent != null)
            deathEvent.Invoke();
        if (respawnPoint.GetComponent<checkpoint_script>().doCam)
        {
            (GameObject.FindGameObjectWithTag("MainCamera")).GetComponent<CameraFollow>().ResetPos(respawnPoint.GetComponent<checkpoint_script>().lookDirection);
        }
    }
    public void ChangeSkin(int id)
    {
        Debug.Log(id);
        skinID = id;
        PlayerPrefs.SetInt("active_skin", id);
        GetComponent<SpriteRenderer>().sprite = skins[skinID];
        GameObject.FindGameObjectWithTag("Phantom").GetComponent<SpriteRenderer>().sprite = skins[skinID];
        Debug.Log(id);
    }
    void Start()
    {
        if(PlayerPrefs.HasKey("max_dashes") == false)
        {
            PlayerPrefs.SetInt("max_dashes", 0);
        }
        if(GetComponent<CharacterController2D>().maxDashes > PlayerPrefs.GetInt("max_dashes"))
        {
            PlayerPrefs.SetInt("max_dashes", GetComponent<CharacterController2D>().maxDashes);
        }

        PlayerPrefs.SetString("ContinueLvl", SceneManager.GetActiveScene().name);
        restartScreen = (GameObject)GameObject.FindGameObjectsWithTag("Restart").GetValue(0);
        ChangeSkin(PlayerPrefs.GetInt("active_skin"));
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "unlock", 1);

        if (PlayerPrefs.HasKey("Coins") == false)
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        if (debug == false)
        {
            respawn(checkpoints[PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "CheckpointIndex")]);
        }

    }
    private void Update()
    {
        if (hp < 0f)
        {
            hp = 0f;
            Death();
        }
        if (healByTime)
        {
            hp += healValue * Time.deltaTime;
            if (hp > 1f)
            {
                hp = 1f;
            }
        }

        if(transform.position.y < -9999)
        {
            Death();
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (doRainDamage)
        {
            hp -= rainDamage;
            if(hp < 0f)
            {
                hp = 0f;
                Death();
            }
        }
    }
    public void Death()
    {
        restartScreen.GetComponent<Canvas>().enabled = true;
        AudioManager.AudioManager.m_instance.PlaySFX(2);
        Time.timeScale = 0;
    }
}

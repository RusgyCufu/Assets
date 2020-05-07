using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class main_script : MonoBehaviour
{
    [SerializeField] public bool debug = true;

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
        if (healByTime)
        {
            hp += healValue * Time.deltaTime;
            if (hp > 1f)
            {
                hp = 1f;
            }
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (doRainDamage)
        {
            hp -= rainDamage;
            if(hp < 0f)
            {
                Death();
                hp = 0f;
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

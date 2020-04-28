using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class main_script : MonoBehaviour
{
    [SerializeField] public bool debug = true;
    
    [Header ("skins")]
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
}

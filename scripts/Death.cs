using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] bool hide = false;
    private GameObject main;
    private GameObject restartScreen;

    private Color oldColor;

    public float dmg = 0.1f;

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
        
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        restartScreen = GameObject.FindGameObjectWithTag("Restart");
        if (hide)
        {
            oldColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            if (hide)
            {
                gameObject.GetComponent<SpriteRenderer>().color = oldColor;
            }
            restartScreen.GetComponent<Canvas>().enabled = true;
            AudioManager.AudioManager.m_instance.PlaySFX(2);
            Time.timeScale = 0;
        }
        else if(collision.gameObject.GetComponent<DamageCat>() != null)
        {
            collision.GetComponent<DamageCat>().Damage(dmg);
        }
    }
}

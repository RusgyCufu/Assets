using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneZero : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] public GameObject displayText;
    [SerializeField] public UnityEvent OnChange;

    private GameObject main;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
            if (value == 0)
            {
                value = 1;
            }
            else if(value == 1)
            {
                value = 0;
            }
            displayText.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }
}
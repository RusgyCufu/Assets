using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneZero : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] public GameObject displayText;
    [SerializeField] public UnityEvent OnChange;
    [SerializeField] public bool changeCables;
    [SerializeField] public GameObject[] cablesOff;
    [SerializeField] public GameObject[] cablesOn;
    [SerializeField] public Color colorOff = new Color(0.4528302f, 0.4528302f, 0.4528302f);
    [SerializeField] public Color colorOn = new Color(0.1682093f, 0.3732349f, 0.5660378f);

    private GameObject main;

    void ChangeCablesColor()
    {
        if (changeCables)
        {
            foreach (GameObject i in cablesOff)
            {
                i.GetComponent<SpriteRenderer>().color = (value == 1 ? colorOff : colorOn);
            }
            foreach (GameObject i in cablesOn)
            {
                i.GetComponent<SpriteRenderer>().color = (value != 1 ? colorOff : colorOn);
            }
        }
    }
    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
        ChangeCablesColor();
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
            ChangeCablesColor();
            displayText.GetComponent<TMPro.TextMeshProUGUI>().text = value.ToString();
            OnChange.Invoke();
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
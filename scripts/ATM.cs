using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ATM : MonoBehaviour
{
    GameObject main;
    dialog dia;
    public int value = 10;
    public UnityEvent doOnBuy;
    public bool buyOnce;
    public int flagBuyOnce = 10;
    public int flagSuccess = 10;
    public int flagNotEnoughMoney = 10;
    public int GOVNo = 10;
    public TMPro.TextMeshProUGUI text;

    public void Buy()
    {
        int coins = main.GetComponent<main_script>().coins;
        if (coins >= value)
        {
            main.GetComponent<main_script>().coins -= value;
            if (doOnBuy != null)
            {
                doOnBuy.Invoke();
            }
            if (buyOnce)
            {
                GetComponent<dialogNPC>().startDialogPointer = flagBuyOnce;
            }
            MovePointer(flagSuccess);
        }
        else
        {
            MovePointer(flagNotEnoughMoney);
        }
    }
    void MovePointer(int flag)
    {
        flag -= 1;
        dia.dialogPointer = flag;
        dia.Display();
    }

    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        dia = GameObject.FindGameObjectWithTag("dialogUi").GetComponent<dialog>();
        text.SetText(value.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

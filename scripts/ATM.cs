﻿using System.Collections;
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
    [Header("forSkins")]
        public bool useForSkins = false;
        public int skinID = 0;
        public GameObject mainATM;

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
    public void Buy()
    {
        int coins = main.GetComponent<main_script>().coins;
        if (coins >= value)
        {
            main.GetComponent<main_script>().coins -= value;
            if (doOnBuy != null)
            {
                if (useForSkins)
                {
                    GetComponent<ActionByVar>().SetName("unlock_skin" + skinID.ToString());
                    GetComponent<ActionByVar>().SetBool(1);
                }
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

    public void SetSkin()
    {
        if (PlayerPrefs.GetInt("unlock_skin" + skinID.ToString()) != 0)
        {
            main.GetComponent<main_script>().ChangeSkin(skinID);
        }
    }
    private void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        dia = GameObject.FindGameObjectWithTag("dialogUi").GetComponent<dialog>();
        text.SetText(value.ToString());
        if (useForSkins)
        {
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().RU = main.GetComponent<main_script>().skinNamesRU[skinID];
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().EN = main.GetComponent<main_script>().skinNamesEN[skinID];
            GetChildWithName(GetChildWithName(mainATM, "Canvas1"), "SkinName").GetComponent<TextLocalization>().Refresh();
            GetChildWithName(GetChildWithName(mainATM, "Canvas"), "Text").GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
            GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
            GetChildWithName(mainATM, "Sprite").GetComponent<SpriteRenderer>().sprite = main.GetComponent<main_script>().skins[skinID];
            GetChildWithName(mainATM, "SpriteB").GetComponent<SpriteRenderer>().sprite = main.GetComponent<main_script>().skins[skinID];
            GetChildWithName(mainATM, "Sprite").GetComponent<ActionByVar>().boolName = "unlock_skin" + skinID.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
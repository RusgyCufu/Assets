using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpVisualizer : MonoBehaviour
{
    GameObject main;
    float hp;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
    }

    // Update is called once per frame
    void Update()
    {
        hp = main.GetComponent<main_script>().hp;
        float tmp = (1f - hp);
        GetComponent<Slider>().SetValueWithoutNotify(tmp);
    }
}

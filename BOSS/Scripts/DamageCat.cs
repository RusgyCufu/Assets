using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCat : MonoBehaviour
{
    public GameObject cat;
    public void Damage(float dmg)
    {
        cat.GetComponent<Boss>().hp -= dmg;
    }
}

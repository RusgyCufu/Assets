using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Range(0f, 40f)] public float viscosity = 20f;
    private GameObject main;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<Rigidbody2D>().velocity -= main.GetComponent<Rigidbody2D>().velocity * viscosity * Time.deltaTime;
            main.GetComponent<CharacterController2D>().jumpsLeft = main.GetComponent<CharacterController2D>().maxJumpCount;
        }
    }
}

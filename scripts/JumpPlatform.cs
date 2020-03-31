using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [Range(1000f, 8000f)] public float force = 2500f;
    private GameObject main;

    void Start()
    {
        main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<CharacterController2D>().Jump(force);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, force));
            Debug.Log(collision.gameObject);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Range(0f, 40f)] public float viscosity = 20f;
    private GameObject main;

    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Stop();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Stop();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<Rigidbody2D>().velocity -= main.GetComponent<Rigidbody2D>().velocity * viscosity * Time.deltaTime;
            main.GetComponent<CharacterController2D>().jumpsLeft = main.GetComponent<CharacterController2D>().maxJumpCount;
            if(GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().isStopped)
            {
                GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "Bubbles").GetComponent<ParticleSystem>().Play();
            }
            //main.GetComponent<CharacterController2D>().dashesLeft = main.GetComponent<CharacterController2D>().maxDashes;
        }
    }
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
}

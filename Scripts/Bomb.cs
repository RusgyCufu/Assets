using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool move;
    public float speed;
    public bool autoTarget;
    public bool targetToMain = true;
    public GameObject target;
    public float damage;
    [Range(0f, 10f)]public float rotationSpeed = 2f;

    private GameObject main;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("main");
        if (targetToMain)
        {
            target = main;
        }
    }

    void FixedUpdate()
    {
        if (autoTarget)
        {
            //Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            Vector3 diff = target.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.fixedDeltaTime * rotationSpeed);
        }

        Vector3 newVel = transform.up * speed;

        if (move == false) newVel = Vector3.zero;

        GetComponent<Rigidbody2D>().velocity = newVel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == main.GetComponent<CapsuleCollider2D>())
        {
            main.GetComponent<main_script>().hp -= damage;
        }
        else if (collision.gameObject.GetComponent<DamageCat>() != null)
        {
            collision.GetComponent<DamageCat>().Damage(damage);
        }
        else if (collision.gameObject.GetComponent<HP>() != null)
        {
            collision.gameObject.GetComponent<HP>().hp -= damage;
        }

            if (collision.isTrigger == false)
        {
            Destroy(gameObject);
        }
        
    }
}

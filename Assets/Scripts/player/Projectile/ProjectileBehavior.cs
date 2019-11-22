using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed;
    Rigidbody controller;
    public float maxDistance;
    public float radius;
    public LayerMask groundMask;
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.velocity = transform.forward * speed * Time.deltaTime;
        Debug.Log("bullet Created");
    }

    // Update is called once per frame
    void Update()
    {
        maxDistance -= speed * Time.deltaTime;
        if (maxDistance < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != gameObject.name.Remove(gameObject.name.Length - 1))
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("ObjectWithLives"))
            {
                Debug.Log("hi");
            }
            Destroy(this.gameObject);
        }
    }

}

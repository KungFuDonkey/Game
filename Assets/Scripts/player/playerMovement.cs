using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    CharacterController controller;
    private Animator animator;
    public float speed;
    public float jumpspeed;
    public float gravity;
    public float mouseSens;
    Vector3 velocity;
    public Transform groundCeck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public int lives = 100;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCeck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        if (isGrounded && velocity.y < 0)
        {
            float y = Input.GetAxis("Jump");
            if (y != 0)
            {
                velocity.y = y * jumpspeed;
            }
            else
            {
                velocity.y = -gravity;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movement * 2 * speed * Time.deltaTime);
            if(movement.x != 0 || movement.z != 0)
                animator.SetInteger("AnimationIndex", 2);
            else
                animator.SetInteger("AnimationIndex", 0);
        }
        else
        {
            controller.Move(movement * speed * Time.deltaTime);
            if (movement.x != 0 || movement.z != 0)
                animator.SetInteger("AnimationIndex", 1);
            else
                animator.SetInteger("AnimationIndex", 0);

        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    public void hit()
    {
        lives -= 20;
    }
}

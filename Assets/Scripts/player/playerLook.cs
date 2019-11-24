﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    public Transform playerbody;
    public Transform camera;
    private Animator animator;
    public Transform projectileSpawner;
    float yRotation = 0f;
    public float mouseSens;
    public float fireTimer = 2, FIRETIMER = 2;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = playerbody.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            Destroy(camera.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);
            fireTimer -= Time.deltaTime;
            if (Input.GetMouseButton(0) && fireTimer < 0)
            {
                animator.SetInteger("AnimationIndex", 3);
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation);
                bullet.name = playerbody.name + "b";
                Debug.Log("Creating Bullet");
                fireTimer = FIRETIMER;
            }
        }
    }
}

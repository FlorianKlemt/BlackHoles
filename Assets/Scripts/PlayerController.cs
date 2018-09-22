﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust;
    public float rotate_speed;
    public float damping;
    public Rigidbody player_rb;
    public bool alive;

    void Start()
    {
        alive = true;
        player_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (alive)
        {
            player_rb.velocity *= damping;
            if (Input.GetKey(KeyCode.A))
                player_rb.AddForce(Vector3.left * thrust);
            //player_rb.transform.Rotate(-Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.D))
                player_rb.AddForce(Vector3.right * thrust);
            //player_rb.transform.Rotate(Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.W))
                player_rb.AddForce(Vector3.forward * thrust);
            if (Input.GetKey(KeyCode.S))
                player_rb.AddForce(Vector3.back * thrust);
        }
        if (player_rb.velocity != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(player_rb.velocity, Vector3.up);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Astroid")
        {
            //TODO explode the astroid?
            Destroy(collision.gameObject);
            //TODO lose?
        }
    }
}

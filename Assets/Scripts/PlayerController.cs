using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust;
    public Rigidbody player_rb;

    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
            player_rb.AddForce(Vector3.left * thrust);
        if (Input.GetKey(KeyCode.D))
            player_rb.AddForce(Vector3.right * thrust);
        if (Input.GetKey(KeyCode.W))
            player_rb.AddForce(Vector3.forward * thrust);
        if (Input.GetKey(KeyCode.S))
            player_rb.AddForce(Vector3.back * thrust);
    }
}

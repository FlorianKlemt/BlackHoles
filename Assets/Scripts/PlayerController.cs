using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust;
    public float rotate_speed;
    public float damping;
    public Rigidbody player_rb;
    public bool alive;
    public Transform applied_shield_prefab;
    private Transform applied_shield;
    public Transform explosion_prefab;

    private PlayerFuel player_fuel;

    void Start()
    {
        alive = true;
        player_rb = GetComponent<Rigidbody>();
        player_fuel = GetComponent<PlayerFuel>();
    }

    void FixedUpdate()
    {
        if (alive && !player_fuel.out_of_fuel())
        {   
            player_rb.velocity *= damping;
            if (Input.GetKey(KeyCode.A))
            {
                player_rb.AddForce(Vector3.left * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            //player_rb.transform.Rotate(-Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.D))
            {
                player_rb.AddForce(Vector3.right * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            //player_rb.transform.Rotate(Vector3.up * rotate_speed * Time.fixedDeltaTime);
            if (Input.GetKey(KeyCode.W))
            {
                player_rb.AddForce(Vector3.forward * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player_rb.AddForce(Vector3.back * thrust);
                player_fuel.useFuel(100.0f * Time.fixedDeltaTime);
            }
            
        }
        if (player_rb.velocity != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(player_rb.velocity, Vector3.up);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Astroid")
        {
            Instantiate(explosion_prefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            //TODO die
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shield")
        {
            applied_shield = Instantiate(applied_shield_prefab, transform.position, new Quaternion(0,0,0,1));
            applied_shield.parent = transform;

            Destroy(other.gameObject);
        }
    }
}

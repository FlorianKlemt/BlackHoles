using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

    public Transform astroid;
    private Transform player;

    // Use this for initialization
    public float min_speed, max_speed, life_time, spawn_cooldown;
    public float min_spawn_distance, max_spawn_distance;
    float time_since_last = 0;
	void Start () {
        player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            return;
        }
        time_since_last += Time.deltaTime;
        if (time_since_last>=spawn_cooldown)
        {
            spawn_asteroid(player.position);
            time_since_last = 0;
        }
	}

    void spawn_asteroid(Vector3 current_coordinates)
    {
        Vector2 spawn_direction = Random.insideUnitCircle.normalized;
        float scaling = Random.Range(min_spawn_distance, max_spawn_distance);
        Vector2 spawn_offset = spawn_direction * scaling;
        Vector3 spawn_point = current_coordinates + new Vector3(spawn_offset.x, 0, spawn_offset.y);

        Vector2 movement_direction = Random.insideUnitCircle.normalized;
        float speed = Random.Range(min_speed, max_speed);

        Transform new_astroid = Instantiate(astroid, spawn_point, Random.rotation);
        new_astroid.GetComponent<Rigidbody>().velocity 
            = new Vector3(movement_direction.x, 0, movement_direction.y) * speed;
        new_astroid.GetComponent<Rigidbody>().angularVelocity 
            = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}

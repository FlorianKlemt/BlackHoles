using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public Transform astroid_explosion_prefab;
    private Transform player;
    public float destroy_distance;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
    }
	
	void Update () {
        if (player != null && Vector3.Distance(transform.position, player.position) > destroy_distance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Astroid")
        {
            Instantiate(astroid_explosion_prefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }else if(other.transform.tag == "BlackHole")
        {
            Destroy(gameObject);
        }
    }
}

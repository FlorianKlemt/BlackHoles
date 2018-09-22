using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidLifetimeHandler : MonoBehaviour {
    public float destroy_distance;

    private Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (player!=null && Vector3.Distance(transform.position, player.position) > destroy_distance)
        {
            Destroy(gameObject);
        }
	}
}

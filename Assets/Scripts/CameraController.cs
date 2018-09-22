using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform player;
    public Vector3 camera_offset;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            return;
        }
        transform.position = player.position + camera_offset;
    }
}

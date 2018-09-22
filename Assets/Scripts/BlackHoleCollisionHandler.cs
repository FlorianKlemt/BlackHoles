using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Astroid")
        {
            Destroy(col.gameObject, 0.2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Astroid")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public int damage;
    public Color color;

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "sol")
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }
	
}

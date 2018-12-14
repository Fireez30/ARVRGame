using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public int damage;
    public Color color;

    public float x = 10;
    public float y = 10;
    public float z = 10;


	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        Vector3 veloc = this.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 angul = this.gameObject.GetComponent<Rigidbody>().angularVelocity;

        if (veloc.x > x) { veloc.x = x; }
        if (veloc.y > y) { veloc.y = y; }
        if (veloc.z > z) { veloc.z = z; }

        if (veloc.x < -x) { veloc.x = -x; }
        if (veloc.y < -y) { veloc.y = -y; }
        if (veloc.z < -z) { veloc.z = -z; }


        this.gameObject.GetComponent<Rigidbody>().velocity = veloc;
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

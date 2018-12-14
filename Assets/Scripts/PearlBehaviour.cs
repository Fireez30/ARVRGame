using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PearlBehaviour : MonoBehaviour {
    public Transform playarea;

    public float x = 10;
    public float y = 10;
    public float z = 10;
    public float maxYDistance = 20f;

    void Start()
    {
        playarea = VRTK_DeviceFinder.PlayAreaTransform();
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

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "sol" && Mathf.Abs(playarea.transform.position.y - gameObject.transform.position.y) < maxYDistance)
        {
            Debug.Log("AH");//executé
            Destroy(gameObject);
            playarea.transform.position = c.contacts[0].point;//pas exécuté
        }

    }
}

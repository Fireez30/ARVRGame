using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PearlBehaviour : MonoBehaviour {
    public Transform playarea;

    public float x;
    public float y;
    public float z;
    public float maxYDistance;
    void Start()
    {
        playarea = VRTK_DeviceFinder.PlayAreaTransform();
        StartCoroutine(Suicide());
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
    
    public IEnumerator Suicide()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "sol" && Mathf.Abs(playarea.transform.position.y - gameObject.transform.position.y) < maxYDistance)
        {
            Destroy(gameObject);
            Vector3 pos = new Vector3();
            foreach(ContactPoint col in c.contacts)
            {
                pos += col.point;
            }
            pos /= c.contacts.Length;
            playarea.transform.position = pos;//pas exécuté
        }

    }
}

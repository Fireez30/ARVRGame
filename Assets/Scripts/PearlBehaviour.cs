using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PearlBehaviour : MonoBehaviour {
    public Transform playarea;

    void Start()
    {
        playarea = VRTK_DeviceFinder.PlayAreaTransform();
    }

	void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "sol")
        {
            Debug.Log("AH");//executé
            Destroy(this.gameObject);//éxecuté
            playarea.transform.position = c.contacts[0].point;//pas exécuté
        }

    }
}

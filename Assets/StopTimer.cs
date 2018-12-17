using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "perle")
        {
            GameObject.FindGameObjectWithTag("gamemanager").GetComponent<Parameters>().counttime = false;
        }
    }
}

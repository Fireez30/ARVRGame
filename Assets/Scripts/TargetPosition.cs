using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour {

    public GameObject GM;
    void OnCollisionEnter(Collision contact)//called each tick of ennemy contact
    {
        if (contact.gameObject.tag == "enemy")//if we're not in cooldown
        {
            GM.GetComponent<Parameters>().Damage(contact.gameObject.GetComponent<EnnemyBehaviour>().damage);//reduce global life
            Destroy(contact.gameObject);//destroy enemy
        }
    }
}

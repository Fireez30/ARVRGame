using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour {

    public int damage;//damage they deal to targets
    public GameObject goal;
    public int speed;//speed of the ennemy
    NavMeshAgent nav;


    void Awake ()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        GameObject[] end = GameObject.FindGameObjectsWithTag("end");
        float max = -1f;
        foreach (GameObject x in end)
        {
            if ((x.transform.position - gameObject.transform.position).magnitude < max)
            {
                max = (x.transform.position - gameObject.transform.position).magnitude;
                goal = x;
            }
        }
        nav.SetDestination(goal.transform.position);
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }

}

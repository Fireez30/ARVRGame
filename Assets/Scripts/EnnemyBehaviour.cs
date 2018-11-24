using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour {

    public int damage;//damage they deal to targets
    public int health;//health of the ennemy
    public GameObject goal;
    public int speed;//speed of the ennemy
    NavMeshAgent nav;
	// Update is called once per frame

    void Awake ()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.SetDestination(new Vector3(0, 0, 0));
    }

    public void ChangeNavDest()
    {
        nav.SetDestination(goal.transform.position);
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Projectile")
        {
            if (health < obj.gameObject.GetComponent<ProjectileBehaviour>().damage)
            {
                health = 0;
                Destroy(this.gameObject);
            }
            else
            {
                health -= obj.gameObject.GetComponent<ProjectileBehaviour>().damage;
                //damage animation
            }
        }
    }
}

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
        StartCoroutine(Suicide());
    }

    public void ChangeNavDest()
    {
        nav.SetDestination(goal.transform.position);
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Projectile")
        {
            if (health <= obj.gameObject.GetComponent<ProjectileBehaviour>().damage)
            {
                Die();
            }
            else
            {
                health -= obj.gameObject.GetComponent<ProjectileBehaviour>().damage;
                //damage animation
            }
        }
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(60);
        Die();
    }
    public void Die()
    {
        goal.GetComponent<TargetPosition>().nbOfEnnemiesAttached--;
        Destroy(this.gameObject);
    }
}

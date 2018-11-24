using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour {

    public int health; //health of the zone the player has to protect

    public int spawnerRadius;//distance where ennemies spawn
    public int nbToSpawn;//initial number to spawn for this level
    public int spawnChance; //each tick, check a dice roll to see if spawn is made

    public GameObject ennemiPrefab;//template ennemi

    public bool invulnerable;//if has been damaged just before, cant be damaged for invulTime seconds
    public float invulTime; //invul time after get damaged

    void Update () {

        int dice = Random.Range(0, 100);
        if (dice < spawnChance && nbToSpawn > 0)
        {
            float angle = Random.Range(0f, 2 * Mathf.PI);//angle sur le cercle
            Vector3 pos = new Vector3(gameObject.transform.position.x + spawnerRadius * Mathf.Cos(angle), gameObject.transform.position.y, gameObject.transform.position.z + spawnerRadius * Mathf.Sin(angle));//position sur le cercle centré sur la postion, de rayon spawnerRadius
            GameObject e =  Instantiate(ennemiPrefab,pos,Quaternion.identity); // création de l'ennemi
            e.GetComponent<EnnemyBehaviour>().goal = this.gameObject;
            e.GetComponent<EnnemyBehaviour>().ChangeNavDest();
            nbToSpawn--;
        }
	}

    void OnCollisionEnter(Collision contact)//called each tick of ennemy contact
    {
        if (!invulnerable)//if we're not in cooldown
        {
            Damage(contact.gameObject.GetComponent<EnnemyBehaviour>().damage);//damage
            StartCoroutine(Cooldown());//start a cooldown
        }
    }

    IEnumerator Cooldown()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    public void Damage(int degats)
    {
        if (health < degats)
        {
            health = 0;
            //signal game loss
            Destroy(this.gameObject);
        }
        else
        {
            health -= degats;
            //damage animation
        }
    }
}

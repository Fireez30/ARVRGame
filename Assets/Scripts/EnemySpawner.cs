using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int nbToSpawn;//initial number to spawn for this level
    public int spawnChance; //each tick, check a dice roll to see if spawn is made
    public GameObject ennemiPrefab;//template ennemi
    public int radius;//where to spawn around 
    public int spawnCooldown; //cooldown of spawn possibility
    public bool stop;

    void Awake()
    {
        stop = false;
    }

    void Update () {
        int dice = Random.Range(0, 100);
        if (dice < spawnChance && nbToSpawn > 0 && !stop)
        {
            int xcord = Random.Range(-radius, radius);
            int zcord = Random.Range(-radius, radius);

            if (xcord == 0)
                xcord = 1;
            if (zcord == 0)
                zcord = 1;

            Vector3 pos = new Vector3(gameObject.transform.position.x + xcord, gameObject.transform.position.y, gameObject.transform.position.z + zcord);//position sur le cercle centré sur la postion, de rayon spawnerRadius
            GameObject e = Instantiate(ennemiPrefab, pos, Quaternion.identity); // création de l'ennemi
            nbToSpawn--;
        }

        if (nbToSpawn == 0)
        {

        }
    }

    IEnumerator Cooldown()
    {
        stop = true;
        yield return new WaitForSeconds(spawnCooldown);
        stop = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour {

    public int goalsLife;
    public int spawnerNumber;
    public int finishedSpawner;
    public bool end;
	// Use this for initialization
	void Awake () {
        finishedSpawner = 0;
        end = false;
	}
	
    void Update()
    {
        if (finishedSpawner == spawnerNumber && !end)
        {
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("enemy");
            if (ennemis.Length == 0)
            {
                Debug.Log("Fin du jeu : success");
                end = true;
            }

        }
    }

    public void Damage(int d)
    {
        if (goalsLife <= d)
        {
            Debug.Log("Fin du jeu : échec");
        }
        else
        {
            goalsLife -= d;
        }
    }

    public void SpawnerFinished()
    {
        finishedSpawner--;
    }
}

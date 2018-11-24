using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour {

    public int goalsLife;
    public int spawnerNumber;
    public int finishedSpawner;
	// Use this for initialization
	void Awake () {
        finishedSpawner = 0;
	}
	
    
    public void Damage(int d)
    {
        if (goalsLife <= d)
        {
            //Fin du jeu
        }
        else
        {
            goalsLife -= d;
        }
    }

    public void SpawnerFinished()
    {
        finishedSpawner--;
        if (finishedSpawner == spawnerNumber)
        {
            //Fin du jeu
        }
    }
}

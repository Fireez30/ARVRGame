using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parameters : MonoBehaviour {

    public int goalsLife;
    int maxLife;
    public int spawnerNumber;
    public int finishedSpawner;
    public int nbtospawn;
    public Text text;
    public bool end;
    public Button start;
	// Use this for initialization
	void Awake () {
        finishedSpawner = 0;
        maxLife = goalsLife;
        end = false;
	}
	
    void FixedUpdate()
    {
        if (finishedSpawner == spawnerNumber && !end)
        {
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("enemy");//boucle couteuse ! trouver un remplacement
            if (ennemis.Length == 0)
            {
                Debug.Log("Fin du jeu : success");
                text.text = "Game is Over";
                end = true;
            }

        }
    }

    public void SetupRestart()
    {
        start.enabled = true;
        text.text = "Game is Over";
        goalsLife = maxLife;
        finishedSpawner = 0;
        end = false;
        start.enabled = false;
        text.text = "";
    }

    public void StartGame()
    {
        SetupRestart();
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
        foreach (GameObject s in spawners)
        {
            s.GetComponent<EnemySpawner>().nbToSpawn = this.nbtospawn;
            s.GetComponent<EnemySpawner>().LevelStart();
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

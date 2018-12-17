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
    public float seconds;
    public int nbOrbsLaunched;
    public Text time;
    public Text boules;
    public bool counttime;
    // Use this for initialization
    void Awake () {
        finishedSpawner = 0;
        maxLife = goalsLife;
        end = false;
        seconds = 0;
        counttime = true;
        nbOrbsLaunched = 0;
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

        if (counttime)
        {
            seconds += Time.fixedDeltaTime;
            if (seconds % 60 >= 10)
                time.text = (int)(seconds / 60) + ":" + (int)(seconds % 60);
            else
                time.text = (int)(seconds / 60) + ":0" + (int)(seconds % 60);
            boules.text = "" + nbOrbsLaunched;
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

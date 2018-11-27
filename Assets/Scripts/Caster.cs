using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Caster : MonoBehaviour {

    public GameObject[] prefabs;// 0 = blue, 1 = tp , 2 = red
    public int selector;// 0 = blue, 1 = tp , 2 = red
    bool loading;//is player loading a cast ?
    bool holding;//is player holding a finished cast ?
    GameObject casting;//object player is casting
    float scalor;//actual scale of the casting

    void Awake()
    {
        loading = false;
        holding = false;
        selector = 0;
        scalor = 0;
    }

    public void TriggerReleased()
    {
        if (loading && scalor != 1) //add controler release
        {
            Debug.Log("Canceled");
            Destroy(casting);
            loading = false;
            //sound effect
        }

        if (holding) //add trigger released to throw
        {
            Debug.Log("Now holding");
            holding = false;
        }
    }

	public void TriggerGestion () {
        if (!loading && !holding) //add controler input detection
        {
            Debug.Log("Trigger pressed for the first time ! ");
            loading = true;
            casting = Instantiate(prefabs[selector], this.gameObject.transform, true);
            if (selector == 0)
            {
                casting.GetComponent<ProjectileBehaviour>().color = Color.blue;
            }
            else if (selector == 2)
            {
                casting.GetComponent<ProjectileBehaviour>().color = Color.red;
            }
            scalor = 0.1f;
            casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
        }
            else if (loading && scalor != 1)
                {
                    Debug.Log("Loading...");
                    scalor += 0.1f;
                    casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
                }

                else if (loading && scalor == 1)
                    {
                        Debug.Log("Loaded");
                        loading = false;
                        holding = true;
                        //sound effect
                    }
	}

    public void ChangeSelect(object o,VRTK.ControllerInteractionEventArgs e)
    {
        Vector2 v = e.touchpadAxis;
        if (v.x > 0.3 && v.x < 0.6 && v.y > 0.9)//Case up = bleu
        {
            selector = 0;
        }
        if (v.y > 0.3 && v.y < 0.6 && v.x < 0.3)//Case left = tp
        {
            selector = 1;
        }
        if (v.y > 0.3 && v.y < 0.6 && v.x > 0.9)//Case right = rouge
        {
            selector = 2;
        }
    }
}

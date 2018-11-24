using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour {

    public GameObject[] prefabs;
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

	// Update is called once per frame
	void Update () {

        if (!loading && !holding) //add controler input detection
        {
            loading = true;
            casting = Instantiate(prefabs[selector], this.gameObject.transform, true);
            scalor = 0.1f;
            casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
        }

        if (loading && scalor != 1) //add controler release
        {
            Destroy(casting);
            loading = false;
        }
        
        if (loading && scalor != 1)
        {
            scalor += 0.1f;
            casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
        }
        else if (loading && scalor == 1)
        {
            loading = false;
            holding = true;
        }

        if (holding) //add trigger released to throw
        {
            //throw
            //holding = false
        }
	}

    public void ChangeSelect(int d)
    {
        switch (d)
        {
            case '0':
                selector = 0;
                break;

            case '1':
                selector = 1;
                break;

            default:
                selector = 2;
                break;
        }
    }
}

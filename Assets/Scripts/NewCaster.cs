using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NewCaster : MonoBehaviour {

    public GameObject[] prefabs;// 0 = blue, 1 = tp , 2 = red
    public int selector;// 0 = blue, 1 = tp , 2 = red
    bool loading;//is player loading a cast ?
    bool holding;//is player holding a finished cast ?
    GameObject casting;//object player is casting
    float scalor;//actual scale of the casting

                 // Use this for initialization
    void Start () {

        loading = false;
        holding = false;
        selector = 0;
        scalor = 0;
        if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
            return;
        }

        GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerClicked);
        GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnclicked);

    }

    private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        if (!loading && !holding) //add controler input detection
        {
            Debug.Log("Trigger pressed for the first time ! ");
            loading = true;
            casting = Instantiate(prefabs[selector], this.gameObject.transform, true);
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

    private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
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
}

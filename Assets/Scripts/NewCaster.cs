using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class NewCaster : MonoBehaviour {

    public Text t;
    public GameObject[] prefabs;// 0 = blue, 1 = tp , 2 = red
    public int selector;// 0 = blue, 1 = tp , 2 = red
    bool loading;//is player loading a cast ?
    bool holding;//is player holding a finished cast ?
    GameObject casting;//object player is casting
    public float scalor;//actual scale of the casting
    public float maxTmer = 0.5f;
    public float speed;
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

        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerClicked);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerUnclicked);
        GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(ChangeSelect2);
    }

    private void FixedUpdate()
    {
        Debug.Log(VRTK_DeviceFinder.GetControllerAngularVelocity(gameObject));
        Debug.Log(VRTK_DeviceFinder.GetControllerVelocity(gameObject));
        if (loading && scalor < 1f)
        {
            Debug.Log("Loading...");
            scalor += Time.fixedDeltaTime;
            scalor = Mathf.Min(1, scalor);
            casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
        }

        else if (loading && scalor >= 1f)
        {
            Debug.Log("Loaded");
            loading = false;
            holding = true;
            //sound effect
        }
        
    }

    private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        if (!loading && !holding) //add controler input detection
        {
            Debug.Log("Trigger pressed for the first time ! ");
            loading = true;
            casting = Instantiate(prefabs[selector],gameObject.transform);
            casting.transform.position = gameObject.transform.position;
            casting.GetComponent<Rigidbody>().useGravity = false;
            if (selector == 0)
            {
                casting.GetComponent<ProjectileBehaviour>().color = Color.blue;
            }
            else if (selector == 2)
            {
                casting.GetComponent<ProjectileBehaviour>().color = Color.red;
            }
            scalor = 0f;
            casting.transform.localScale.Scale(new Vector3(scalor, scalor, scalor));
        }

    }

    private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
    {
        if (!holding) //add controler release
        {
            Debug.Log("Canceled");
            Destroy(casting);
            loading = false;
            holding = false;
            //sound effect
        }

        if (holding) //add trigger released to throw
        {
            Debug.Log("Now holding");
            casting.transform.parent = null;
            casting.GetComponent<Rigidbody>().useGravity = true;
            casting.GetComponent<Rigidbody>().angularVelocity = VRTK_DeviceFinder.GetControllerAngularVelocity(gameObject);
            casting.GetComponent<Rigidbody>().velocity = VRTK_DeviceFinder.GetControllerVelocity(gameObject)*speed;
            holding = false;
            loading = false;
        }
    }

    private void ChangeSelect2(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        Vector2 v = e.touchpadAxis;
        v.Normalize();
        Debug.Log("Vector axis x :" + v.x+ " y : "+v.y);
        if (v.x > 0.7)//Case droite = pearl
        {
            Debug.Log("Vector axis :"+v.x);
            t.text = "pearl";
            selector = 1;
        }
        if (v.y > 0.7)//Case up = bleu
        {
            t.text = "bleu";
            Debug.Log("Changer vers bleu !");
            selector = 0;
        }
        if (v.x < -0.7)//Case gauche = rouge
        {
            t.text = "rouge";
            Debug.Log("Changer vers rouge !");
            selector = 2;
        }
    }
}

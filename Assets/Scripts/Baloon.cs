using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour {

	public bool isRed;

	private Vector3 memPos, memScale;
	private GameObject childPlatform;
	private GameObject balloon;
	// Use this for initialization

	void Awake () {
		childPlatform = gameObject.transform.GetChild (0).gameObject;
		childPlatform.SetActive (false);
		balloon = gameObject.transform.GetChild (1).gameObject;
		memPos = childPlatform.transform.position;
		memScale = childPlatform.transform.localScale;
		if(isRed)
			balloon.GetComponent<MeshRenderer>().material.color = Color.red;
		else
			balloon.GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	void OnCollisionEnter(Collision obj)
	{
		Debug.Log ("Collision : " + obj.gameObject);
		bool red = obj.gameObject.GetComponent<ProjectileBehaviour>().color == Color.red;
		if (obj.gameObject.tag == "Projectile" && red==isRed)
		{
			StartCoroutine (activatePlatform ());
            Destroy(obj.gameObject);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            balloon.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
	}

	public IEnumerator activatePlatform(){
		childPlatform.SetActive (true);
		Vector3 parentPos = gameObject.transform.position;
		float t = 0;
		while (t <= 0.5) {
			childPlatform.transform.localScale = Vector3.Lerp (new Vector3 (0, 0, 0), memScale, t*2);
			childPlatform.transform.position = Vector3.Lerp (parentPos, memPos, t*2);
           // Debug.Log("While ! pos x : " + childPlatform.transform.position.x+" y : "+ childPlatform.transform.position.y + " z : " + childPlatform.transform.position.z);
            t += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime); 
		}
		yield break;
	}
}

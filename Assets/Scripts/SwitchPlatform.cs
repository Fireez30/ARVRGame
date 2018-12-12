using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : MonoBehaviour {

	public List<GameObject> rouge;
	public List<GameObject> bleu;

	private GameObject prism;

	public static bool RougeActif;
	private static SwitchPlatform Instance;
	private static List<SwitchPlatform> AllInstances;

	private static float timer;
	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			RougeActif = true;
			Instance = this;
			AllInstances = new List<SwitchPlatform> ();
		}
		AllInstances.Add (this);
		prism = gameObject.transform.GetChild (0).gameObject;
		updateColor();

		timer = 0;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (SwitchPlatform.timer > 0) {
			timer -=Time.fixedDeltaTime;
		}
	}

	public void updateColor(){
		if (SwitchPlatform.IsRedActive ()) {
			prism.GetComponent<MeshRenderer> ().material.color = Color.red;
		} else {
			prism.GetComponent<MeshRenderer> ().material.color = Color.blue;
		}
	}

	private static bool IsRedActive(){
		return SwitchPlatform.RougeActif;
	}
	private static void ChangePlatform(){
		if (SwitchPlatform.timer <= 0) {
			timer = 1f;
			SwitchPlatform.RougeActif = !(SwitchPlatform.RougeActif);
			foreach (SwitchPlatform s in AllInstances) {
				s.updateColor ();
				foreach (GameObject g in s.rouge) {
					g.SetActive (RougeActif);
				}
				foreach (GameObject g in s.bleu) {
					g.SetActive (!RougeActif);
				}
			}
		}
	}

	void OnCollisionEnter(Collision obj)
	{
		Debug.Log ("Collision : " + obj.gameObject);
		if (obj.gameObject.tag == "Projectile")
		{
			Destroy(obj.gameObject);
			SwitchPlatform.ChangePlatform ();
		}
	}
}

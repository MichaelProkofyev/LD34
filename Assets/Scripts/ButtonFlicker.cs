using UnityEngine;
using System.Collections;

public class ButtonFlicker : MonoBehaviour {

	public GameObject arrowLeft;
	public GameObject arrowRight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Right") || Input.GetButtonDown("Left")) {
			Destroy(gameObject);
		}
	}


	public IEnumerator StartFlicker () {
		while(true) {
			arrowLeft.SetActive(false);
			arrowRight.SetActive(false);
			yield return new WaitForSeconds(0.5f);
			arrowLeft.SetActive(true);
			arrowRight.SetActive(true);
			yield return new WaitForSeconds(0.5f);
		}
	}
}

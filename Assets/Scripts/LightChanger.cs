using UnityEngine;
using System.Collections;

public class LightChanger : MonoBehaviour {

	public int lightIndex = 0;

	public Sprite [] lightSprites;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
//		StartCoroutine("ChangeLights");
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public IEnumerator ChangeLights () {
		while(true) {
			yield return new WaitForSeconds(0.5f);
			lightIndex = Random.Range(0, 4);
			spriteRenderer.sprite = lightSprites[lightIndex];
		}
	}
}

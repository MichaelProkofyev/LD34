﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ComboController : MonoBehaviour {

	Text comboText;
	Vector3 maxLocalScale = Vector3.one * 2;
	Color comboColorHot;// = ToColor("2ECC71");
	float coolTime = .5f;
	float currcoolTime;
	public int comboPoints = 1;

	// Use this for initialization
	void Start () {
		comboColorHot = new Color(231/255f, 76/255f, 60/255f);
	}

	void Awake () {
		comboText = GetComponent<Text> ();

	}

	void Update () {
		if (currcoolTime > 0) {
			currcoolTime -= Time.deltaTime;
			comboText.transform.localScale = Vector3.Lerp(Vector3.one, maxLocalScale, currcoolTime/coolTime);
		}
	}

	public void AddComboPoint () {
		comboText.color = comboColorHot;//Color.red;
		currcoolTime = coolTime;
		comboPoints += 1;
		comboText.text = "Combo X" + comboPoints;
	}

	public void ResetComboPoints () {
		comboText.color = Color.white;
		comboPoints = 1;
		comboText.text = "Combo X" + comboPoints;
	}

}

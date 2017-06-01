using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public class CtgryButtonAttachment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CtgryClick()
    {
        LevelEngine engine = Camera.main.GetComponent<LevelEngine>();
        GetComponent<AudioSource>().Play();
        engine.ToDetail(gameObject.name);
    }
    public void BtnClick()
    {
        SandboxEngine engine = Camera.main.GetComponent<SandboxEngine>();
        engine.OpenCtgry(gameObject.name);
    }
}

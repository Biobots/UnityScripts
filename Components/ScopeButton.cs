using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scope;
using UnityEngine.UI;
using Assets.Scripts;

public class ScopeButton : MonoBehaviour {
    public string itemName;
    public string picName;
    public string sysName;
    public GameObject pic;
    public GameObject map;
	// Use this for initialization
	void Start () {
        map = GameObject.Find("Map");
        pic = GameObject.Find("Map1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BtnClick()
    {
        ScopeSys.currentChapter = sysName;
        ScopeSys.currentItem = itemName;
        Texture2D t = (Texture2D)Instantiate(Resources.Load("Scope/" + ScopeSys.content[itemName].picName));
        pic.GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        map.GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        //pic.GetComponent<SpriteRenderer>().sprite = t;
        //map.GetComponent<Image>().sprite = t;
        GameObject.Find("Description").GetComponent<Text>().text = ScopeSys.content[itemName].intro;
        GameObject.Find("Title").GetComponent<Text>().text = itemName;
    }
}

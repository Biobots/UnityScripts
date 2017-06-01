using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class TestScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalSys.XmlInitialize();
        foreach (var f in GlobalSys.ElementIndex)
        {
            LoadElement(f.Value, f.Value.Position);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
    void LoadElement(Element target, Vector3 position)
    {
        target.isChosen = false;
        target.isShown = true;

        target.Value = Instantiate(Resources.Load("Prefabs/" + target.Key), position, new Quaternion(0, 0, 0, 0)) as GameObject;
        target.Value.name = target.Key;

    }

}

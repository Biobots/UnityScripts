using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class MainProcess : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GlobalSys.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
   public  void Search()
    {
        string input = GameObject.Find("SearchInput").GetComponent<UnityEngine.UI.Text>().text;
        Listener.instance
            .Let(input)
            .Activate(SearchFunc.Search);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MouseFunc
    {
        public static readonly MouseFunc list = new MouseFunc();

        public static Observer MouseOverObject = new Observer();
        public static Observer MouseExitObject = new Observer();
        private MouseFunc() { }
        public static void Awake()
        {
            MouseOverObject.Update += OverInfo;
            MouseExitObject.Update += ExitInfo;
        }
        #region Functions
        #region MouseOverObjects
        private static void OverInfo(EventArgs e)
        {
            GameObject pointer = (GameObject)e.sender;
            GameObject label = (GameObject)e.reciever;
            label.GetComponent<Text>().text = pointer.name;
            pointer.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        private static void OverState(EventArgs e)
        {
            GameObject pointer = (GameObject)e.sender;
            GlobalSys.GetElement(pointer).CurrentState.Add(State.Hovered);
            GlobalSys.GetElement(pointer).CurrentState.Add(State.Highlighted);
            Listener.instance
                .From(GlobalSys.GetElement(pointer))
                .Activate(StateFunc.Hover);
        }
        #endregion
        #region MouseExitObjects
        private static void ExitInfo(EventArgs e)
        {
            GameObject pointer = (GameObject)e.sender;
            GameObject label = (GameObject)e.reciever;
            label.GetComponent<Text>().text = "Buttom";
            //Material mat = new Material(Shader.Find("LightShader1"));
            pointer.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        private static void ExitState(EventArgs e)
        {
            GameObject pointer = (GameObject)e.sender;
            GlobalSys.GetElement(pointer).CurrentState.Remove(State.Hovered);
            Listener.instance
                .From(GlobalSys.GetElement(pointer))
                .Activate(StateFunc.Hover);
        }
        #endregion
        #endregion
    }

    public class StateFunc
    {
        public static readonly StateFunc list = new StateFunc();
        private StateFunc() { }

        public static Observer Hover = new Observer();
        public static void Awake()
        {

        }
        #region Functions
        #region Hover
        private static void StateEffect(EventArgs e)
        {
            Element pointer = (Element)e.sender;
            if(pointer.CurrentState.Contain(State.Highlighted))
            {
                pointer.Value.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                pointer.Value.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        #endregion
        #region Exit

        #endregion
        #endregion

    }

    public class SearchFunc
    {
        public static readonly SearchFunc list = new SearchFunc();
        private SearchFunc() { }

        public static Observer Search = new Observer();
        public static void Awake()
        {

        }
        #region Functions
        private static void StartSearch(EventArgs e)
        {
            string input = (string)e.sender;
            Dictionary<string, Element> temp = (Dictionary<string, Element>)e.reciever;
            GlobalSys.Results.Add("Search:" + input, SearchSys<Element>.Search(input, temp));
        }
        #endregion
    }


}

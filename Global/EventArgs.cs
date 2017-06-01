using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EventArgs
    {
        public object sender;
        public object reciever;
    }


    public class StringEventArgs
    {
        private string str;
        public StringEventArgs(string str)
        {
            this.str = str;
        }
        public string Str
        {
            get { return str; }
        }
    }
    public class GameObjectEventArgs
    {
        private GameObject go;
        public GameObjectEventArgs(GameObject go)
        {
            this.go = go;
        }
        public GameObject Go
        {
            get { return go; }
        }
    }
    public class ElementEventArgs
    {
        private Element Element;
        private List<Element> ElementList;
        public ElementEventArgs(Element Element)
        {
            this.Element = Element;
        }
        public ElementEventArgs(List<Element> Elements)
        {
            this.ElementList = Elements;
        }
        public Element ChosenElement
        {
            get { return Element; }
        }
        public List<Element> Elements
        {
            get { return ElementList; }
        }
    }
    public class SearchListEventArgs          //<T> where T : ISearchable<GameObject>
    {
        private string input;
        public SearchListEventArgs(string input)
        {
            this.input = input;
        }
        public string Input
        {
            get { return input; }
        }
    }
    public class MouseEventArgs
    {
        private GameObject pointer;
        public MouseEventArgs(GameObject pointer)
        {
            this.pointer = pointer;
        }
        public GameObject Pointer
        {
            get { return pointer; }
        }
    }
}

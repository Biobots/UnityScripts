using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Element : ISearchableData<string>, IComparable/*, IEnumerable<Element>*/
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Category { get; set; }
        public State CurrentState { get; set; }
        public GameObject Value { get; set; }
        public Vector3 Position { get; set; }
        public List<Element> Members { get; set; }
        public Element Parent
        {
            get
            {
                if (GlobalSys.InheritIndex[Key].Parent != null) { return GlobalSys.InheritIndex[Key].Parent; }
                else { return null; }
            }
        }
        public List<Element> Children
        {
            get
            {
                if (GlobalSys.InheritIndex[Key].Children != null) { return GlobalSys.InheritIndex[Key].Children; }
                else { return null; }
            }
        }
        public Element CoreChild
        {
            get
            {
                if (GlobalSys.InheritIndex[Key].CoreChild != null) { return GlobalSys.InheritIndex[Key].CoreChild; }
                else { return null; }
            }
        }


        public bool isShown { get; set; }
        public bool isChosen { get; set; }
        public bool isLinkedToCore { get; set; }
        public bool isCoreChild { get; set; }
        public bool isInPosition { get; set; }


        public int SortNum { get; set; }
        public void ConvertToValue(string[] input)
        {
            int max= input.Length - 1;
            for (int index = 0; index <= max; index++)
            {
                GlobalSys.PropIndex[index]
                    .SetValue(this, Convert.ChangeType(input[index], GlobalSys.PropIndex[index].PropertyType), null);
            }
        }

        public int CompareTo(object obj)
        {
            Element e = (Element)obj;
            return SortNum.CompareTo(e.SortNum);
        }

        //public IEnumerator<Element> GetEnumerator()
        //{
        //    return ((IEnumerable<Element>)Member).GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable<Element>)Member).GetEnumerator();
        //}
    }
}

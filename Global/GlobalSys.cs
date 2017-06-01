using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
    static public class GlobalSys
    {
        static public int SearchResultCount = 20;
        static public float MinFixDistance = 3;

        static public string Root = Environment.CurrentDirectory;
        static public string App = Application.dataPath;

        static public Dictionary<string, Element> ElementIndex = new Dictionary<string, Element>();
        static public Dictionary<string, Inherit> InheritIndex = new Dictionary<string, Inherit>();
        static public Dictionary<string, VecPair> VectorIndex = new Dictionary<string, VecPair>();
        static public Dictionary<int, PropertyInfo> PropIndex = new Dictionary<int, PropertyInfo>();

        static public Element currentChosen;
        static public Dictionary<string, List<Element>> Results = new Dictionary<string, List<Element>>();

        static public List<GameObject> RightPanels = new List<GameObject>();

        static public List<List<Element>> ConnectedList = new List<List<Element>>();

        static public void Initialize()
        {
            GetProperties();

            ElementIndex = Loader<string, Element>.LoadData(App + "/StreamingAssets/Info.csv");
            InheritIndex = Loader<string, Inherit>.LoadData(App + "/StreamingAssets/Inherit.csv");
            VectorIndex = Loader<string, VecPair>.LoadData(App + "/StreamingAssets/Vector.csv");

            foreach(Element e in ElementIndex.Values)
            {
                if (e.CoreChild != null) { e.CoreChild.isCoreChild = true; }
            }

            MouseFunc.Awake();
        }
        static public void XmlInitialize()
        {
            XmlLoader temp = new XmlLoader();
            //temp.LoadInfo(App + "/StreamingAssets/anatomy.xml");
            temp.LoadInfo(Root + "/anatomy2.xml");
        }
        static private void GetProperties()
        {
            int index = 0;
            string tempstr;
            PropertyInfo temppro;
            Type type = typeof(PropType);
            Type element = typeof(Element);
            do
            {
                tempstr = Enum.GetName(type, index);
                temppro = element.GetProperty(tempstr, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                PropIndex.Add(index, temppro);
                index++;
            } while (Enum.IsDefined(type, index));
        }

        static public Element GetElement(GameObject target)
        {
            return GlobalSys.ElementIndex[target.name];
        }
        static public Vector3 CalculateVector(Element start, Element end)
        {
            return end.Value.transform.position - start.Value.transform.position;
        }
        static public Vector3 SearchVector(Element start, Element end)
        {
            string temp = start.Key + "to" + end.Key;
            //if (VectorIndex.ContainsKey(temp))
            { return VectorIndex[temp].Value; }
            //else
            //{ throw new KeyNotFoundException(); }
        }
        static public bool TryLinkChild(Element pointer, Element core)
        {
            Vector3 currentCtoP = CalculateVector(core, pointer);
            Vector3 standardCtoP = SearchVector(core, pointer);
            float distance = Vector3.Distance(currentCtoP, standardCtoP);
            if (distance <= MinFixDistance)
            {
                pointer.Value.transform.parent = core.Value.transform;
                pointer.Value.transform.localPosition = standardCtoP;
                return true;
            }
            else { return false; }
        }
        static public List<Element> GetList(Element Key)
        {
            foreach (List<Element> list in ConnectedList)
            {
                if (list.Contains(Key))
                {
                    return list;
                }
            }
            return null;
        }

    }
    public delegate void SearchListEventHandler(SearchListEventArgs e);
    public delegate void MouseEventHandler(MouseEventArgs e);
    public delegate void StringEventHandler(StringEventArgs e);
    public delegate void GameObjectEventHandler(GameObjectEventHandler e);
    public delegate void ElementEventHandler(ElementEventArgs e);
    public delegate void VoidEventHandler();
}

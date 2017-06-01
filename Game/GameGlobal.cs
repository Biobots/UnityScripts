using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class GameGlobal
    {
        static public int MainScrore = 0;
        static public Dictionary<string, int> AreaScrore = new Dictionary<string, int>();
        static public bool Initialized = false;

        static public Dictionary<string, List<Element>> areas;
        static public Dictionary<string, List<Element>> completed = new Dictionary<string, List<Element>>();

        static public string CurrentCategory = "腹部";
        static public bool GameSelectLock = false;
        static public bool SandboxLock = false;
        static public bool StructureLock = false;
        static public bool GameLock = false;

        static public void Initialize()
        {
            GlobalSys.XmlInitialize();
            areas = new Dictionary<string, List<Element>>();
            foreach (Element e in GlobalSys.ElementIndex.Values)
            {
                if (e.Category == null)
                { break; }
                else if (areas.ContainsKey(e.Category))
                { areas[e.Category].Add(e); }
                else
                {
                    List<Element> temp = new List<Element>();
                    temp.Add(e);
                    areas.Add(e.Category, temp);
                }
            }
            foreach (string ctgry in areas.Keys)
            {
                completed.Add(ctgry, new List<Element>());
            }
            Initialized = true;
        }
    }
}

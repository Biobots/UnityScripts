using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts
{
    class XmlLoader
    {
        public XmlLoader()
        {
            
        }
        XmlDocument doc;

        public void LoadInfo(string path)
        {
            doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Collection/Element");
            foreach (XmlNode node in nodes)
            { FillInfo(node); }
            foreach (XmlNode node in nodes)
            { FillInherit(node); }
            FillVector();
        }

        void FillInfo(XmlNode Xparent)
        {
            Element nd = new Element();
            foreach (XmlNode content in Xparent.ChildNodes)
            {
                if (content.Name == "key")
                { nd.Key = content.InnerText; }
                else if (content.Name == "name")
                { nd.Name = content.InnerText; }
                else if (content.Name == "info")
                { nd.Info = content.InnerText; }
                else if (content.Name == "category")
                { nd.Category = content.InnerText; }
                else if (content.Name == "position")
                {
                    float x = 0, y = 0, z = 0;
                    foreach (XmlElement e in content.ChildNodes)
                    {
                        if (e.Name == "x")
                        { x = float.Parse(e.InnerText); }
                        if (e.Name == "y")
                        { y = float.Parse(e.InnerText); }
                        if (e.Name == "z")
                        { z = float.Parse(e.InnerText); }
                    }
                    nd.Position = new Vector3(x, y, z);
                }
                else if (content.Name == "Element")
                {
                    FillInfo(content);
                }
            }
            GlobalSys.ElementIndex.Add(nd.Key, nd);
        }
        void FillInherit(XmlNode Xparent)
        {
            Inherit ih = new Inherit();
            XmlNode core = Xparent.SelectSingleNode("/Element");
            if (core == null) { return; }
            foreach (XmlNode content in core)
            {
                if (content.Name == "key")
                {
                    ih.CoreChild = GlobalSys.ElementIndex[content.InnerText];
                    break;
                }
            }
            foreach (XmlNode content in Xparent.ChildNodes)
            {
                if (content.Name == "key")
                { ih.Key = content.InnerText; }
            }
            foreach (XmlNode content in Xparent.ChildNodes)
            {
                if (content.Name == "Element")
                {
                    FillInherit(content);
                    foreach (XmlNode x in content)
                    {
                        if (x.Name == "key")
                        {
                            GlobalSys.InheritIndex[x.InnerText].Parent = GlobalSys.ElementIndex[ih.Key];
                            ih.Children.Add(GlobalSys.ElementIndex[x.InnerText]);
                            break;
                        }
                    }
                }
            }
        }
        public static Vector3 GetVector(Element start, Element end)
        {
            return end.Position - start.Position;
        }
        public static void FillVector()
        {
            foreach (Element value in GlobalSys.ElementIndex.Values)
            {
                foreach (Element target in GlobalSys.ElementIndex.Values)
                {
                    if (value != target)
                    {
                        VecPair temp = new VecPair();
                        temp.Value = value.Position - target.Position;
                        temp.Key = target.Key + "to" + value.Key;
                        GlobalSys.VectorIndex.Add(temp.Key, temp);
                    }
                }
            }
        }
    }
}

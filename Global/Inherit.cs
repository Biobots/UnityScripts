using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inherit : IData<string>
    {
        public string Key { get; set; }

        public Element Parent { get; set; }
        public Element CoreChild { get; set; }
        public List<Element> Children = new List<Element>();

        public void ConvertToValue(string[] input)
        {
            try
            {
                Key = input[0];
                if (input[1] != "") { Parent = GlobalSys.ElementIndex[input[1]]; }
                else { Parent = null; }
                CoreChild = GlobalSys.ElementIndex[input[2]];
                int max = input.Length - 1;
                for (int i = 3; i <= max; i++)
                { Children.Add(GlobalSys.ElementIndex[input[i]]); }
            }
            catch
            {
                return;
            }
        }
    }
}

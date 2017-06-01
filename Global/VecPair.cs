using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class VecPair : IData<string>
    {
        public string Key { get; set; }
        public Vector3 Value { get; set; }

        public VecPair()
        {  }

        public void ConvertToValue(string[] input)
        {
            Key = input[0] + "to" + input[1];
            Value = new Vector3(float.Parse(input[2]), float.Parse(input[3]), float.Parse(input[4]));
        }
    }
}

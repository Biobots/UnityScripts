using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    interface IData<T>
    {
        T Key { get; set; }
        void ConvertToValue(string[] input);
    }

    interface ISearchableData<T> : IData<T>
    {
        GameObject Value { get; set; }
        int SortNum { get; set; }
    }
}

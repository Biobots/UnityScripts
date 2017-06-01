using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Assets.Scripts
{
    class Loader<U, T> where T : IData<U>, new()
    {
        public static Dictionary<U,T> LoadData(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string strLine = "";
            string[] aryLine;
            Dictionary<U, T> tempDic = new Dictionary<U, T>();
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                T value = new T();
                value.ConvertToValue(aryLine);
                tempDic.Add(value.Key, value);
            }
            return tempDic;
        }
    }
}

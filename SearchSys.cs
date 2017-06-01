using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    static class SearchSys<T> where T : ISearchableData<string>
    {
        static public List<T> Search(string input, Dictionary<string, T> dic)
        {
            List<T> results = new List<T>();
            foreach (string value in dic.Keys)
            {
                dic[value].SortNum = Edit(input, value);
                results.Add(dic[value]);
            }
            results.Sort();
            List<T> final = new List<T>();
            for (int i=0;i<GlobalSys.SearchResultCount;i++)
            {
                final.Add(results.First());
                results.RemoveAt(0);
            }
            final.Sort();
            return final;
        }

        static private int Edit(string a, string b)
        {
            if (a.Length == 0 && b.Length == 0) { return 0; }
            else if (a.Length == 0 && b.Length > 0) { return b.Length; }
            else if (b.Length == 0 && a.Length > 0) { return a.Length; }
            else
            {
                int alpha = Edit(a.Substring(0, a.Length - 1), b) + 1;
                int beta = Edit(a, b.Substring(0, b.Length - 1)) + 1;
                int gamma = Edit(a.Substring(0, a.Length - 1), b.Substring(0, b.Length - 1))
                    + Compare(GetChar(a, a.Length - 1), GetChar(b, b.Length - 1));
                return Math.Max(Math.Max(alpha, beta), gamma);
            }
            //int[,] Matrix;
            //int n = a.Length;
            //int m = b.Length;
            //int temp = 0;
            //char chr1, chr2;
            //int j = 0, i = 0;
            //if(n==0)
            //{
            //    return m;
            //}
            //if(m==0)
            //{
            //    return n;
            //}
            //Matrix = new int[n + 1, m + 1];
            //for(i=0;i<=n;i++)
            //{
            //    Matrix[i, 0] = i;
            //}
            //for(j=0;j<= m;i++)
            //{
            //    Matrix[0, j] = j;
            //}
            //for(i=1;1<= n;i++)
            //{
            //    chr1 = a[i - 1];
            //    for(j=1;j<= m;j++)
            //    {
            //        chr2 = b[j - 1];
            //        if (chr1.Equals(chr2))
            //        { temp = 0; }
            //        else
            //        { temp = 1; }
            //        Matrix[i, j] = GetLower(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1, Matrix[i - 1, j - 1] + temp);
            //    }
            //}
            //return Matrix[n, m];
        }
        static int GetLower(int a,int b, int c)
        {
            int min = Math.Min(a, b);
            return Math.Min(min, c);
        }
        static private char GetChar(string str, int index)
        { return Char.Parse(str.Substring(index, 1)); }
        static private int Compare(char a, char b)
        {
            if (a == b) { return 0; }
            else { return 1; }
        }
    }
}

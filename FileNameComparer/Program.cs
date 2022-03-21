using System;
using System.Collections;
using System.Collections.Generic;

namespace FileNameComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            IComparer fileNameComparer = new FilesNameComparerClass();
            ArrayList list = new ArrayList(4);
            list.Add("１００號");
            list.Add("１１號");
            list.Add("２號");
            list.Add("３１１號");
            list.Add("１０８號之２");
            list.Add("１０８９號");
            list.Sort(fileNameComparer);
            foreach(string l in list)
            {
                Console.WriteLine(l);
            }
        }

    }
    ///<summary>
    ///主要用於文件名的比較。
    ///</summary>
    public class FilesNameComparerClass : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        ///<summary>
        ///比較兩個字符串，如果含用數字，則數字按數字的大小來比較。
        ///</summary>
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns></returns>
        int IComparer.Compare(Object x, Object y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Parameters can't be null");
            string fileA = x as string;
            string fileB = y as string;
            char[] arr1 = fileA.ToCharArray();
            char[] arr2 = fileB.ToCharArray();
            int i = 0, j = 0;
            while (i < arr1.Length && j < arr2.Length)
            {
                if (char.IsDigit(arr1[i]) && char.IsDigit(arr2[j]))
                {
                    string s1 = "", s2 = "";
                    while (i < arr1.Length && char.IsDigit(arr1[i]))
                    {
                        s1 += arr1[i];
                        i++;
                    }
                    while (j < arr2.Length && char.IsDigit(arr2[j]))
                    {
                        s2 += arr2[j];
                        j++;
                    }
                    s1 = ToDBC(s1);//轉半形
                    s2 = ToDBC(s2);//轉半形
                    if (int.Parse(s1) > int.Parse(s2))
                    {
                        return 1;
                    }
                    if (int.Parse(s1) < int.Parse(s2))
                    {
                        return -1;
                    }
                }
                else
                {
                    if (arr1[i] > arr2[j])
                    {
                        return 1;
                    }
                    if (arr1[i] < arr2[j])
                    {
                        return -1;
                    }
                    i++;
                    j++;
                }
            }
            if (arr1.Length == arr2.Length)
            {
                return 0;
            }
            else
            {
                return arr1.Length > arr2.Length ? 1 : -1;
            }
            //            return string.Compare( fileA, fileB );
            //            return( (new CaseInsensitiveComparer()).Compare( y, x ) );
        }
        /// <summary>
        /// 轉全形的函數(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ToSBC(string input)
        {
            //半形轉全形：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  轉半形的函數(SBC case)
        /// </summary>
        /// <param name="input">輸入</param>
        /// <returns></returns>
        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
    }
}

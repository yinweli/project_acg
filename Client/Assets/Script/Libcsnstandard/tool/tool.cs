/**
 * @file tool.cs
 * @note 工具組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief 工具類別
     * @ingroup tools
     */
    public class Tool
    {
        //-------------------------------------
        /**
         * @brief unicode字串轉換成json字串
         * @param szInput 輸入字串
         */
        public static string unicode_to_json(string szInput)
        {
            string szResult = "";
            char[] ArrayChar = szInput.ToCharArray();

            foreach (char c in ArrayChar)
            {
                if ((int)c > 127)
                {
                    byte[] ArrayByte = Encoding.Unicode.GetBytes(c.ToString());
                    szResult += "\\u" + ArrayByte[1].ToString("x2") + ArrayByte[0].ToString("x2");
                }
                else
                    szResult += c;
            }//for

            return szResult;
        }
        /**
         * @brief json字串轉換成unicode字串
         * @param szInput 輸入字串
         */
        public static string json_to_unicode(string szInput)
        {
            string szResult = "";
            byte[] cToken = new byte[2];

            for (int iPos = 0, iMax = szInput.Length; iPos < iMax; )
            {
                if (szInput[iPos] == '\\' && szInput[iPos + 1] == 'u')
                {
                    cToken[1] = Convert.ToByte(szInput.Substring(iPos + 2, 2), 16);
                    cToken[0] = Convert.ToByte(szInput.Substring(iPos + 4, 2), 16);
                    szResult += Encoding.Unicode.GetString(cToken);
                    iPos += 6;
                }
                else
                {
                    szResult += szInput[iPos];
                    ++iPos;
                }//if
            }//for

            return szResult;
        }
        /**
         * @brief swap
         * @param lhs 左邊物件
         * @param rhs 右邊物件
         */
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T v = lhs;

            lhs = rhs;
            rhs = v;
        }
        /**
         * @brief 隨機排列陣列元素
         * @param array 陣列物件
         */
        public static void RandomShuffle<T>(T[] array)
        {
            int iCount = array.Length;

            while (iCount > 1)
            {
                iCount--;
				Swap(ref array[iCount], ref array[RandObject.Next(iCount + 1)]);
            }//while
        }
        /**
         * @brief 隨機排列列表元素
         * @param list 列表物件
         */
        public static void RandomShuffle<T>(List<T> list)
        {
            int iCount = list.Count;
            int iRand = 0;

            while (iCount > 1)
            {
                iCount--;
				iRand = RandObject.Next(iCount + 1);

                T v = list[iCount];

                list[iCount] = list[iRand];
                list[iRand] = v;
            }//while
        }
        /**
         * @brief 隨機取得列表內容
         * @param list 列表物件
         */
        public static T RandomPick<T>(List<T> list)
        {
			return list.Count > 0 ? list[RandObject.Next(list.Count)] : default(T);
        }
        /**
         * @brief 隨機取得列表內容
         * @param set 列表物件
         */
        public static T RandomPick<T>(HashSet<T> set)
        {
            int iRand = RandObject.Next(set.Count);
            int iPos = 0;

            foreach (T Itor in set)
            {
                if (iPos != iRand)
                    ++iPos;
                else
                    return Itor;
            }//for

            return default(T);
        }
        /**
         * @brief 隨機取得列表內容
         * @param dir 列表物件
         */
        public static KeyValuePair<K, V> RandomPick<K, V>(Dictionary<K, V> dir)
        {
            int iRand = RandObject.Next(dir.Count);
            int iPos = 0;

            foreach (KeyValuePair<K, V> Itor in dir)
            {
                if (iPos != iRand)
                    ++iPos;
                else
                    return Itor;
            }//for

            return new KeyValuePair<K, V>();
        }
        /**
         * @brief 組合列表
         * @param list 列表物件陣列
         * @return 結果列表
         */
        public static List<T> CombineList<T>(List<T>[] list)
        {
            List<T> Result = new List<T>();

            foreach (List<T> Itor in list)
            {
                foreach (T ItorList in Itor)
                    Result.Add(ItorList);
            }//for

            return Result;
        }
        /**
         * @brief 組合路徑
         * @param Path 路徑字串陣列
         * @return 路徑字串
         */
        public static string CombinePath(string[] Path)
        {
            string szResult = "";

            foreach (string Itor in Path)
                szResult += szResult.Length <= 0 ? Itor : ("/" + Itor);

            return szResult;
        }
        /**
         * @brief 寫入文字檔案
         * @param szPath 檔案路徑
         * @param Data 檔案內容列表
         */
        public static void SaveTextFile(string szPath, List<string> Data)
        {
            File.WriteAllLines(szPath, Data.ToArray(), Encoding.Unicode);
        }
        /**
         * @brief 讀取文字檔案
         * @param szPath 檔案路徑
         * @return 檔案內容列表物件
         */
        public static List<string> LoadTextFile(string szPath)
        {
            try
            {
                return new List<string>(File.ReadAllLines(szPath));
            }
            catch
            {
                return null;
            }
        }
        /**
         * @brief 執行外部程式
         * @param szFileName 程式名稱
         * @param szParam 參數字串
         * @return true表示成功, false則否
         */
        public static bool Execute(string szFileName, string szParam)
        {
            Process pc = new Process();

            pc.StartInfo.FileName = szFileName;
            pc.StartInfo.Arguments = szParam;

            return pc.Start();
        }
        /**
		 * @brief 物件內容輸出函式
		 * @param Object 物件
		 * @return 結果字串
		 */
        public static string Content(IList Object)
        {
            string szResult = "";

            szResult += "{ ";

            foreach (object Itor in Object)
                szResult += Itor + ", ";

            szResult += " }";

            return szResult;
        }
        /**
         * @brief 物件內容輸出函式
         * @param Object 物件
         * @return 結果字串
         */
        public static string Content(IDictionary Object)
        {
            string szResult = "";

            szResult += "{ ";

            foreach (DictionaryEntry Itor in Object)
                szResult += "(" + Itor.Key + ":" + Itor.Value + "), ";

            szResult += " }";

            return szResult;
        }
        /**
         * @brief 物件內容輸出函式
         * @param Object 物件
         * @return 結果列表
         */
        public static List<string> Content(object Object)
        {
            List<string> Result = new List<string>();
            FieldInfo[] flist = Object.GetType().GetFields();

            foreach (FieldInfo finfo in flist)
            {
                string fname = finfo.Name;
                object fdata = finfo.GetValue(Object);

                if (fdata == null)
                    continue;

                if (fdata is Array || fdata is IList)
                {
                    Result.Add(fname + " = " + Content((IList)fdata));
                    continue;
                }//if

                if (fdata is IDictionary)
                {
                    Result.Add(fname + " = " + Content((IDictionary)fdata));
                    continue;
                }//if

                if (fdata.GetType().IsPrimitive)
                {
                    Result.Add(fname + " = " + fdata);
                    continue;
                }//if

                if (fdata is string)
                {
                    Result.Add(fname + " = " + fdata);
                    continue;
                }//if

                foreach (string Itor in Content(fdata))
                    Result.Add(Itor);
            }//for

            return Result;
        }
        /**
         * @brief 物件內容輸出函式
         * @param Object 物件
         * @return 結果字串
         */
        public static string ContentString(object Object)
        {
            string szResult = "";

            foreach (string Itor in Content(Object))
                szResult += Itor + "; ";

            return szResult;
        }
        /**
         * @brief 取得型態名稱
         * @return 型態名稱
         */
        public static string TypeName<T>()
        {
            return typeof(T).ToString();
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
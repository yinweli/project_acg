/**
 * @file dbfdata.cs
 * @note dbf資料組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_IPHONE || UNITY_ANDROID
#define UNITY
#endif
//-----------------------------------------------------------------------------
#if UNITY
using UnityEngine;
#endif
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief dbf資料介面
     */
    public interface DBFInterface
    {
        //-------------------------------------
        /**
         * @brief 讀取dbf
         * @param szFilePath dbf檔名路徑
         * @return true表示成功, false則否
         */
        bool Load(string szFilePath);
        /**
         * @brief 讀取dbf
         * @return true表示成功, false則否
         */
        bool Load();
        /**
         * @brief 取得資料物件
         * @param GUID 索引物件
         * @return 資料物件
         */
        DBF Get(Argu GUID);
        /**
         * @brief 取得資料數量
         * @return 資料數量
         */
        int Count();
        /**
         * @brief 取得檔名路徑
         * @return 檔名路徑
         */
        string FilePath();
        /**
         * @brief 取得索引列表
         * @return 索引列表
         */
        List<string> List();
        //-------------------------------------
    }
    /**
     * @brief dbf單元類別
     */
    public class DBFUnit<T> : DBFInterface where T : DBF
    {
        //-------------------------------------
        private string m_szFilePath = ""; // dbf檔名路徑
        private Dictionary<string, T> m_Data = new Dictionary<string, T>(); // dbf資料列表 <索引字串, dbf物件>
        //-------------------------------------
        /**
         * @brief 讀取dbf
         * @param szFilePath dbf檔名路徑
         * @return true表示成功, false則否
         */
        public bool Load(string szFilePath)
        {
            m_szFilePath = szFilePath;

            return Load();
        }
        /**
         * @brief 讀取dbf
         * @return true表示成功, false則否
         */
        public bool Load()
        {
            m_Data.Clear();

            if (m_szFilePath.Length <= 0)
                return Output.Error(this, "dbf filepath empty");

#if UNITY
            TextAsset FileReader = Resources.Load(m_szFilePath) as TextAsset;

            if (FileReader == null)
                return Output.Error(this, "dbf read failed");

            string[] DataList = FileReader.text.Split(new char[] {'\n'});

            foreach (string szData in DataList)
            {
                T Data = Json.ToObject<T>(szData);

                if (Data != null)
                    m_Data.Add(Data.GUID, Data);
            }//for
#else
            StreamReader FileReader = new StreamReader(m_szFilePath, System.Text.Encoding.Default);

            if (FileReader == null)
                return Output.Error(this, "dbf read failed");

            string szData = "";

            while ((szData = FileReader.ReadLine()) != null)
            {
                T Data = Json.ToObject<T>(szData);

                if (Data != null)
                    m_Data.Add(Data.GUID, Data);
            }//while

            FileReader.Close();
#endif

            if (m_Data.Count <= 0)
                return Output.Error(this, "dbf empty");

            return true;
        }
        /**
         * @brief 取得資料物件
         * @param GUID 索引物件
         * @return 資料物件
         */
        public DBF Get(Argu GUID)
        {
            return m_Data.ContainsKey(GUID) ? m_Data[GUID] : null;
        }
        /**
         * @brief 取得資料數量
         * @return 資料數量
         */
        public int Count()
        {
            return m_Data.Count;
        }
        /**
         * @brief 取得檔名路徑
         * @return 檔名路徑
         */
        public string FilePath()
        {
            return m_szFilePath;
        }
        /**
         * @brief 取得索引列表
         * @return 索引列表
         */
        public List<string> List()
        {
            return new List<string>(m_Data.Keys);
        }
        //-------------------------------------
    }
    /**
     * @brief dbf資料類別
     */
    public class DBFData : IEnumerable
    {
        //-------------------------------------
        private Dictionary<string, DBFInterface> m_Data = new Dictionary<string, DBFInterface>(); // dbf資料物件列表 <dbf名稱, dbf單元物件>
        //-------------------------------------
        public IEnumerator GetEnumerator()
        {
            return m_Data.GetEnumerator();
        }
        //-------------------------------------
        /**
		 * @brief 新增dbf
		 * @param szName dbf名稱
		 * @param szFilePath 檔名路徑
		 * @return true表示成功, false則否
		 */
        public bool Add<T>(string szName, string szFilePath) where T : DBF
        {
            DBFInterface Data = new DBFUnit<T>();

            if (Data.Load(szFilePath) == false)
                return Output.Error(this, "add dbf failed [" + szName + ":" + szFilePath + "]");

            m_Data.Add(szName, Data);

            return true;
        }
        /**
         * @brief 新增dbf
		 * @param szName dbf名稱
         * @param szClass 類別名稱(包括命名空間與類別名稱, 用 . 分隔)
         * @param szComponent 組件名稱
         * @param szFilePath 檔名路徑
         * @return true表示成功, false則否
         */
        public bool Add(string szName, string szClass, string szComponent, string szFilePath)
        {
            try
            {
                string szType = szClass + ", " + szComponent + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                Type TypeDBF = Type.GetType(szType);

                if (TypeDBF == null)
                    throw new Exception("can't found dbf type(" + szType + ")");

                Type TypeGeneric = typeof(DBFUnit<>).MakeGenericType(new Type[] { TypeDBF });

                if (TypeGeneric == null)
                    throw new Exception("make generic type failed(" + szType + ")");

                DBFInterface Data = (DBFInterface)Activator.CreateInstance(TypeGeneric);

                if (Data == null)
                    throw new Exception("create dbf loader failed(" + szType + ")");

                if (Data.Load(szFilePath) == false)
                    throw new Exception("load failed");

                m_Data.Add(szName, Data);

                return true;
            }//try

            catch (Exception e)
            {
                return Output.Error(this, "add dbf filed [" + szName + "] [" + e.Message + "]");
            }//catch
        }
        /**
         * @brief 重新讀取dbf
         * @return true表示成功, false則否
         */
        public bool Reload()
        {
            bool bResult = true;

            foreach (KeyValuePair<string, DBFInterface> Itor in m_Data)
                bResult &= Itor.Value.Load();

            return bResult;
        }
        /**
         * @brief 重新讀取dbf
         * @param szName dbf名稱
         * @return true表示成功, false則否
         */
        public bool Reload(string szName)
        {
            return m_Data.ContainsKey(szName) ? m_Data[szName].Load() : false;
        }
        /**
         * @brief 取得dbf
         * @param szName dbf名稱
         * @param GUID 索引物件
         * @return dbf物件
         */
        public DBF Get(string szName, Argu GUID)
        {
            return m_Data.ContainsKey(szName) ? m_Data[szName].Get(GUID) : null;
        }
        /**
         * @brief 取得dbf迭代器
         * @param szName dbf名稱
         * @return dbf迭代器物件
         */
        public DBFItor Get(string szName)
        {
            return new DBFItor(m_Data.ContainsKey(szName) ? m_Data[szName] : null);
        }
        /**
         * @brief 取得資料數量
         * @param szName dbf名稱
         * @return 資料數量
         */
        public int Count(string szName)
        {
            return m_Data.ContainsKey(szName) ? m_Data[szName].Count() : 0;
        }
        /**
         * @brief 取得檔名路徑
         * @param szName dbf名稱
         * @return 檔名路徑
         */
        public string FilePath(string szName)
        {
            return m_Data.ContainsKey(szName) ? m_Data[szName].FilePath() : "";
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
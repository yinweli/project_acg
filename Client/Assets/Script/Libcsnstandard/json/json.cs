/**
 * @file json.cs
 * @note json組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief json資料類別
     */
    public class JsonData
    {
        //-------------------------------------
        private Dictionary<string, object> m_Data = new Dictionary<string, object>();
        //-------------------------------------
        public JsonData(string json)
        {
            m_Data = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
        }
        public JsonData(object Obj)
        {
            m_Data = Obj as Dictionary<string, object>;
        }
        //-------------------------------------
        private Argu ToArgu(string szName, object Obj)
        {
            if (Obj == null)
                throw new Exception(szName + " is null");
            else if (Obj.GetType() == typeof(bool))
                return new Argu((bool)Obj);
            else if (Obj.GetType() == typeof(char))
                return new Argu((char)Obj);
            else if (Obj.GetType() == typeof(byte))
                return new Argu((byte)Obj);
            else if (Obj.GetType() == typeof(sbyte))
                return new Argu((sbyte)Obj);
            else if (Obj.GetType() == typeof(short))
                return new Argu((short)Obj);
            else if (Obj.GetType() == typeof(ushort))
                return new Argu((ushort)Obj);
            else if (Obj.GetType() == typeof(int))
                return new Argu((int)Obj);
            else if (Obj.GetType() == typeof(uint))
                return new Argu((uint)Obj);
            else if (Obj.GetType() == typeof(long))
                return new Argu((long)Obj);
            else if (Obj.GetType() == typeof(ulong))
                return new Argu((ulong)Obj);
            else if (Obj.GetType() == typeof(float))
                return new Argu((float)Obj);
            else if (Obj.GetType() == typeof(double))
                return new Argu((double)Obj);
            else if (Obj.GetType() == typeof(string))
                return new Argu((string)Obj);
            else if (Obj.GetType() == typeof(Array) || Obj.GetType() == typeof(IList))
                throw new Exception(szName + " is array/list");
            else if (Obj.GetType() == typeof(IDictionary))
                throw new Exception(szName + " is dictionary");
            else
                throw new Exception(szName + " is class/object");
        }
        //-------------------------------------
        /**
         * @brief 取得資料成員
         * @param szName 名稱
         * @return 資料物件
         */
        public Argu Get(string szName)
        {
            return ToArgu(szName, m_Data[szName]);
        }
        /**
         * @brief 取得json資料成員
         * @param szName 名稱
         * @return json資料物件
         */
        public JsonData GetData(string szName)
        {
            object Obj = m_Data[szName];

            if (Obj == null)
                throw new Exception(szName + " is null");
            else if (Obj.GetType() == typeof(Dictionary<string, object>))
                return new JsonData(Obj);
            else
                throw new Exception(szName + " not class/object");
        }
        /**
         * @brief 取得列表成員
         * @param szName 名稱
         * @return 列表物件
         */
        public List<Argu> GetListArgument(string szName)
        {
            object Obj = m_Data[szName];

            if (Obj == null)
                throw new Exception(szName + " is null");
            else if (Obj.GetType() == typeof(Array) || Obj.GetType() == typeof(IList))
            {
                List<Argu> Result = new List<Argu>();

                foreach (object Itor in Obj as IList)
                    Result.Add(ToArgu(szName, Itor));

                return Result;
            }
            else
                throw new Exception(szName + " not array/list");
        }
        /**
         * @brief 取得列表成員
         * @param szName 名稱
         * @return 列表物件
         */
        public List<T> GetListTemplate<T>(string szName)
        {
            object Obj = m_Data[szName];

            if (Obj == null)
                throw new Exception(szName + " is null");
            else if (Obj.GetType() == typeof(Array) || Obj.GetType() == typeof(IList))
            {
                List<T> Result = new List<T>();

                foreach (object Itor in Obj as IList)
                    Result.Add((T)Itor);

                return Result;
            }
            else
                throw new Exception(szName + " not array/list");
        }
        /**
         * @brief 取得json資料列表成員
         * @param szName 名稱
         * @return 列表物件
         */
        public List<JsonData> GetListData(string szName)
        {
            object Obj = m_Data[szName];

            if (Obj == null)
                throw new Exception(szName + " is null");
            else if (Obj.GetType() == typeof(List<object>))
            {
                List<JsonData> Result = new List<JsonData>();

                foreach (object Itor in Obj as IList)
                    Result.Add(new JsonData(Itor));

                return Result;
            }
            else
                throw new Exception(szName + " not array/list");
        }
        /**
         * @brief 取得是否為空
         * @return true表示為空, false則否
         */
        public bool Empty()
        {
            return m_Data.Count <= 0;
        }
        //-------------------------------------
    }
    /**
     * @brief json類別
     */
    public class Json
    {
        //-------------------------------------
        /**
         * @brief 由json字串取得json資料物件
         * @param szJson json字串
         * @return json資料物件
         */
        public static JsonData ToJsonData(string szJson)
        {
            return szJson.Length > 0 ? new JsonData(szJson) : null;
        }
        /**
         * @brief 由json字串取得物件
         * @param szJson json字串
         * @return 物件
         */
        public static T ToObject<T>(string szJson)
        {
            DeJson.Deserializer deserializer = new DeJson.Deserializer();

            return szJson.Length > 0 ? deserializer.Deserialize<T>(szJson) : default(T);
        }
        /**
         * @brief 由物件取得json字串
         * @param Obj 物件
         * @return json字串
         */
        public static string ToString(object Obj)
        {
            return Obj != null ? DeJson.Serializer.Serialize(Obj, false) : "";
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
/**
 * @file argument.cs
 * @note 參數組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief 參數基礎類別
     */
    public class Argu
    {
        //-------------------------------------
        private string m_szValue = ""; // 參數資料
        //-------------------------------------
        public Argu() { }
        public Argu(bool value)
        {
            m_szValue = value ? "1" : "0";
        }
        public Argu(char value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(byte value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(sbyte value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(short value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(ushort value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(int value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(uint value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(long value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(ulong value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(float value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(double value)
        {
            m_szValue = Convert.ToString(value);
        }
        public Argu(string value)
        {
            m_szValue = value;
        }
        //-------------------------------------
        public static implicit operator Argu(bool value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(char value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(byte value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(sbyte value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(short value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(ushort value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(int value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(uint value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(long value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(ulong value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(float value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(double value)
        {
            return new Argu(value);
        }
        public static implicit operator Argu(string value)
        {
            return new Argu(value);
        }
        //-------------------------------------
        public static implicit operator bool(Argu value)
        {
            return value.m_szValue != "0";
        }
        public static implicit operator char(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToChar(value.m_szValue) : (char)0;
        }
        public static implicit operator byte(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToByte(value.m_szValue) : (byte)0;
        }
        public static implicit operator sbyte(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToSByte(value.m_szValue) : (sbyte)0;
        }
        public static implicit operator short(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToInt16(value.m_szValue) : (short)0;
        }
        public static implicit operator ushort(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToUInt16(value.m_szValue) : (ushort)0;
        }
        public static implicit operator int(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToInt32(value.m_szValue) : 0;
        }
        public static implicit operator uint(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToUInt32(value.m_szValue) : 0;
        }
        public static implicit operator long(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToInt64(value.m_szValue) : 0;
        }
        public static implicit operator ulong(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToUInt64(value.m_szValue) : 0;
        }
        public static implicit operator float(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToSingle(value.m_szValue) : 0.0f;
        }
        public static implicit operator double(Argu value)
        {
            return value.m_szValue.Length > 0 ? Convert.ToDouble(value.m_szValue) : 0.0f;
        }
        public static implicit operator string(Argu value)
        {
            return value.m_szValue;
        }
        //-------------------------------------
    }
    /**
     * @brief 映對式參數類別
     */
    public class Argp
    {
        //-------------------------------------
        private Dictionary<string, Argu> m_Argument = new Dictionary<string, Argu>(); ///< 參數列表 <參數名稱, 參數資料>
        //-------------------------------------
        public Argp() { }
        public Argp(Argp argp) { m_Argument = argp.m_Argument; }
        //-------------------------------------
        /**
         * @brief 清除全部
         */
        public void Clear()
        {
            m_Argument.Clear();
        }
        /**
         * @brief 設定參數
         * @param szName 參數名稱
         * @param value 參數值
         */
        public void Set(string szName, Argu value)
        {
            m_Argument[szName] = value;
        }
        /**
         * @brief 設定參數
         * @param szName 參數名稱
         * @param value 參數值
         */
        public void Set(Argp value)
        {
            foreach (KeyValuePair<string, Argu> itor in value.m_Argument)
                m_Argument[itor.Key] = itor.Value;
        }
        /**
         * @brief 取得參數
         * @param szName 參數名稱
         * @return 參數資料
         */
        public Argu Get(string szName)
        {
            return m_Argument.ContainsKey(szName) ? m_Argument[szName] : new Argu();
        }
        /**
         * @brief 取得參數列表
         * @return 參數列表
         */
        public Dictionary<string, Argu> Get()
        {
            return m_Argument;
        }
        /**
         * @brief 取得資料數量
         * @return 資料數量
         */
        public int Size()
        {
            return m_Argument.Count;
        }
        /**
         * @brief 取得是否為空
         * @return true表示為空, false則否
         */
        public bool Empty()
        {
            return m_Argument.Count == 0;
        }
        /**
         * @brief 取得參數是否存在
         * @param szName 參數名稱
         * @return true表示存在, false則否
         */
        public bool IsExist(string szName)
        {
            return m_Argument.ContainsKey(szName);
        }
        //-------------------------------------
    }
    /**
     * @brief 序列式參數類別
     */
    public class Argv
    {
        //-------------------------------------
        private List<Argu> m_Argument = new List<Argu>();
        //-------------------------------------
        public Argv() { }
        public Argv(Argv argv) { m_Argument = argv.m_Argument; }
        public Argv(bool[] data) { foreach (bool Itor in data) m_Argument.Add(Itor); }
        public Argv(char[] data) { foreach (char Itor in data) m_Argument.Add(Itor); }
        public Argv(byte[] data) { foreach (byte Itor in data) m_Argument.Add(Itor); }
        public Argv(sbyte[] data) { foreach (sbyte Itor in data) m_Argument.Add(Itor); }
        public Argv(short[] data) { foreach (short Itor in data) m_Argument.Add(Itor); }
        public Argv(ushort[] data) { foreach (ushort Itor in data) m_Argument.Add(Itor); }
        public Argv(int[] data) { foreach (int Itor in data) m_Argument.Add(Itor); }
        public Argv(uint[] data) { foreach (uint Itor in data) m_Argument.Add(Itor); }
        public Argv(long[] data) { foreach (long Itor in data) m_Argument.Add(Itor); }
        public Argv(ulong[] data) { foreach (ulong Itor in data) m_Argument.Add(Itor); }
        public Argv(float[] data) { foreach (float Itor in data) m_Argument.Add(Itor); }
        public Argv(double[] data) { foreach (double Itor in data) m_Argument.Add(Itor); }
        public Argv(string[] data) { foreach (string Itor in data) m_Argument.Add(Itor); }
        //-------------------------------------
        public static implicit operator Argv(bool[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(char[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(byte[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(sbyte[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(short[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(ushort[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(int[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(uint[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(long[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(ulong[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(float[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(double[] data)
        {
            return new Argv(data);
        }
        public static implicit operator Argv(string[] data)
        {
            return new Argv(data);
        }
        //-------------------------------------
        /**
         * @brief 清除全部
         */
        public void Clear()
        {
            m_Argument.Clear();
        }
        /**
         * @brief 設定參數
         * @param iPos 參數位置
         * @param value 參數值
         */
        public void Set(int iPos, Argu value)
        {
            while (m_Argument.Count < iPos)
                m_Argument.Add(new Argu());

            m_Argument.Add(value);
        }
        /**
         * @brief 設定參數
         * @param value 參數值
         */
        public void Set(Argu value)
        {
            m_Argument.Add(value);
        }
        /**
         * @brief 設定參數
         * @param value 參數值
         */
        public void Set(Argv value)
        {
            foreach (Argu argu in value.m_Argument)
                m_Argument.Add(argu);
        }
        /**
         * @brief 取得參數
         * @param iPos 參數位置
         * @return 參數資料
         */
        public Argu Get(int iPos)
        {
            return m_Argument.Count > iPos ? m_Argument[iPos] : new Argu();
        }
        /**
         * @brief 取得全部參數
         * @return 全部參數列表
         */
        public List<Argu> Get()
        {
            return m_Argument;
        }
        /**
         * @brief 取得資料數量
         * @return 資料數量
         */
        public int Size()
        {
            return m_Argument.Count;
        }
        /**
         * @brief 取得是否為空
         * @return true表示為空, false則否
         */
        public bool Empty()
        {
            return m_Argument.Count == 0;
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
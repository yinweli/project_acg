/**
 * @file dbfitor.cs
 * @note dbf迭代器組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief dbf列舉類別
     */
    public class DBFItor
    {
        //-------------------------------------
        private DBFInterface m_Data = null; // 資料物件
        private List<string> m_List = null; // 索引列表
        private int m_iPos = 0; // 目前位置
        //-------------------------------------
        public DBFItor(DBFInterface data)
        {
            if (data == null)
                return;

            m_Data = data;
            m_List = data.List();
            m_iPos = 0;
        }
        //-------------------------------------
        private bool Check()
        {
            return m_Data != null && m_List != null;
        }
        //-------------------------------------
        /**
         * @brief 取得dbf
         * @return dbf物件
         */
        public DBF Data()
        {
            return Check() && m_iPos < m_List.Count ? m_Data.Get(m_List[m_iPos]) : null;
        }
        /**
         * @brief 移動到下一個
         */
        public void Next()
        {
            if (Check() && m_iPos < m_List.Count)
                ++m_iPos;
        }
        /**
         * @brief 取得否到結尾
         * @return true表示到結尾, false則否
         */
        public bool IsEnd()
        {
            return Check() == false || m_iPos >= m_List.Count;
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
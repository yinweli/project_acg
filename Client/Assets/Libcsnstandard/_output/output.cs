/**
 * @file _output.cs
 * @note 錯誤輸出類別
 * @author yinweli
 */
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
    /**
     * @brief 錯誤輸出類別
     */
    public class Output
    {
        //-------------------------------------
        private static Queue<string> m_Data = new Queue<string>(); // 錯誤訊息列表
        private static Object m_Lock = new Object(); // 執行緒鎖
        //-------------------------------------
        /**
         * @brief 錯誤輸出
         * @param Object 錯誤訊息物件
         * @param szError 錯誤字串
         * @return 只會回傳false
         */
        public static bool Error(object Object, string szError)
        {
            lock (m_Lock)
                m_Data.Enqueue("[" + DateTime.Now + "] " + (Object != null ? Object.ToString() : "") + " : " + szError);

            return false;
        }
        /**
         * @brief 取得錯誤訊息
         * @return 錯誤訊息
         */
        public static string Get()
        {
            lock (m_Lock)
                return m_Data.Count > 0 ? m_Data.Dequeue() : "";
        }
        //-------------------------------------
    }
}
//-----------------------------------------------------------------------------
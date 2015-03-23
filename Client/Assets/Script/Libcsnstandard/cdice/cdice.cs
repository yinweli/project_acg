/**
 * @file cdice.cs
 * @note 可變式多面骰組件
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
     * @brief 可變式多面骰類別
     * @ingroup tools
     */
	public class CDice<T> : IEnumerable
	{
		//-------------------------------------
		private Dictionary<T, int> m_Data = new Dictionary<T, int>(); // 骰子列表<內容值, 機率值>
		private int m_iMax = 0; // 最大機率值
		//-------------------------------------
		public CDice() {}
		public CDice(Dictionary<T, int> data)
		{
			m_Data = data;
		}
		public IEnumerator GetEnumerator()
		{
			return m_Data.GetEnumerator();
		}
		//-------------------------------------
		/**
		 * @brief 清除全部
		 */
		public void Clear()
		{
			m_Data.Clear();
		}
		/**
		 * @brief 設定內容
		 * @param Data 內容值
		 * @param iProb 機率值
		 */
		public void Set(T Data, int iProb)
		{
			m_Data[Data] = iProb;
			m_iMax = 0;
			
			foreach(KeyValuePair<T, int> Itor in m_Data)
				m_iMax += Itor.Value;
		}
		/**
		 * @brief 刪除內容
		 * @param Data 內容值
		 */
		public void Del(T Data)
		{
			m_Data.Remove(Data);
		}
		/**
		 * @brief 丟骰子
		 * @return 內容值
		 */
		public T Roll()
		{
			return Roll(new Random());
		}
		/**
		 * @brief 丟骰子
		 * @param Rand 亂數物件
		 * @return 內容值
		 */
		public T Roll(Random Rand)
		{
			if(Rand == null)
				return default(T);

			if(m_Data.Count <= 0)
				return default(T);

			int iDice = Rand.Next(m_iMax);

			foreach(KeyValuePair<T, int> Itor in m_Data)
			{
				if((iDice -= Itor.Value) < 0)
					return Itor.Key;
			}//for

			return default(T);
		}
		//-------------------------------------
	}
}
//-----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CDice<T> : IEnumerable
{
	private Dictionary<T, int> m_Data = new Dictionary<T, int>(); // 骰子列表<內容值, 機率值>
	private int m_iMax = 0; // 最大機率值

	public CDice() {}
	public CDice(Dictionary<T, int> data)
	{
		m_Data = data;
	}
	public IEnumerator GetEnumerator()
	{
		return m_Data.GetEnumerator();
	}
	// 清除全部
	public void Clear()
	{
		m_Data.Clear();
	}
	// 設定內容
	public void Set(T Data, int iProb)
	{
		m_Data[Data] = iProb;
		m_iMax = 0;
		
		foreach(KeyValuePair<T, int> Itor in m_Data)
			m_iMax += Itor.Value;
	}
	// 刪除內容
	public void Del(T Data)
	{
		m_Data.Remove(Data);
	}
	// 丟骰子
	public T Roll()
	{
		if(m_Data.Count <= 0)
			return default(T);
		
		int iDice = Random.Range(0, m_iMax);
		
		foreach(KeyValuePair<T, int> Itor in m_Data)
		{
			if((iDice -= Itor.Value) < 0)
				return Itor.Key;
		}//for
		
		return default(T);
	}
}
using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class ResourceStat : MonoBehaviour
{
	static public ResourceStat pthis = null;

	public List<int> Init = new List<int>(); // 初始資源
	public List<int> Gain = new List<int>(); // 獲得資源
	public List<int> Used = new List<int>(); // 使用資源

	void Awake()
	{
		pthis = this;
	}
	void Start()
	{
		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Resource)))
		{
			Init.Add(0);
			Gain.Add(0);
			Used.Add(0);
		}//for
	}
	public void Reset()
	{
		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Resource)))
		{
			if(PlayerData.pthis.Resource.Count > Itor)
				Init[Itor] = PlayerData.pthis.Resource[Itor];

			Gain[Itor] = 0;
			Used[Itor] = 0;
		}//for
	}
	public void Record(ENUM_Resource emResource, int iValue)
	{
		if(iValue == 0)
			return;

		if(iValue > 0)
			Gain[(int)emResource] += Mathf.Abs(iValue);
		else
			Used[(int)emResource] += Mathf.Abs(iValue);
	}
	public void Report()
	{
		string szReport = "";

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Resource)))
		{
			if(Itor == (int)ENUM_Resource.Null)
				continue;
			
			if(Itor == (int)ENUM_Resource.Resource_Count)
				continue;

			szReport += string.Format("resource[{0}] : start({1}), gain({2}), used({3})\n", (ENUM_Resource)Itor, Init[Itor], Gain[Itor], Used[Itor]);
		}//for

		Debug.Log(szReport);
	}
}

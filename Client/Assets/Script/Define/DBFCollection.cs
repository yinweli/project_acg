using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DBFCollection : DBF
{
	public int Name = 0; // 收集名稱
	public string Lv1Items = ""; // 等級1收集編號列表
	public string Lv2Items = ""; // 等級2收集編號列表
	public string Lv3Items = ""; // 等級3收集編號列表
	public string Lv4Items = ""; // 等級4收集編號列表
	public string Lv5Items = ""; // 等級5收集編號列表
	public string Lv6Items = ""; // 等級6收集編號列表
	public int Lv1Reward = 0; // 等級1獎勵編號
	public int Lv2Reward = 0; // 等級2獎勵編號
	public int Lv3Reward = 0; // 等級3獎勵編號
	public int Lv4Reward = 0; // 等級4獎勵編號
	public int Lv5Reward = 0; // 等級5獎勵編號
	public int Lv6Reward = 0; // 等級6獎勵編號

	private List<int> ToList(string szItems)
	{
		List<int> Result = new List<int>();
		string[] szTemp = szItems.Split(new char[] {','});

		foreach(string Itor in szTemp)
			Result.Add(System.Convert.ToInt32(Itor));

		return Result;
	}
	// 取得收集編號列表
	public List<int> Items(int iLevel)
	{
		switch(iLevel)
		{
		case 1: return ToList(Lv1Items);
		case 2: return ToList(Lv2Items);
		case 3: return ToList(Lv3Items);
		case 4: return ToList(Lv4Items);
		case 5: return ToList(Lv5Items);
		case 6: return ToList(Lv6Items);
		default: return new List<int>();
		}//switch
	}
}
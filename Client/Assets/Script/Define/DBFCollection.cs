﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DBFCollection : DBF
{
	public int Name = 0; // 收集名稱
	public int Items1 = 0; // 收集物品編號1
	public int Items2 = 0; // 收集物品編號2
	public int Items3 = 0; // 收集物品編號3
	public int Items4 = 0; // 收集物品編號4
	public int Items5 = 0; // 收集物品編號5
	public int Reward = 0; // 獎勵列舉
	public int RewardValue = 0; // 獎勵值

	public static string GetGUID(int iGUID, int iLevel)
	{
		return iGUID + "-" + iLevel;
	}
	public static Tuple<int, int> GetGUID(string szGUID)
	{
		string[] szTemp = szGUID.Split(new char[] {'-'});
		
		return szTemp.Length >= 2 ? new Tuple<int, int>(System.Convert.ToInt32(szTemp[0]), System.Convert.ToInt32(szTemp[1])) : null;
	}
}
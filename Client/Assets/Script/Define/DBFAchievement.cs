﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DBFAchievement : DBF
{
	/* 
	 * 成就DBF的GUID就是ENUM_Achievement的編號
	 */

	public int Name = 0; // 成就名稱
	public int MaxLevel = 0; // 最大等級
	public int Lv1Value = 0; // 等級1條件值
	public int Lv2Value = 0; // 等級2條件值
	public int Lv3Value = 0; // 等級3條件值
	public int Lv4Value = 0; // 等級4條件值
	public int Lv5Value = 0; // 等級5條件值
	public int Lv6Value = 0; // 等級6條件值
	public int Lv1Reward = 0; // 等級1獎勵編號
	public int Lv2Reward = 0; // 等級2獎勵編號
	public int Lv3Reward = 0; // 等級3獎勵編號
	public int Lv4Reward = 0; // 等級4獎勵編號
	public int Lv5Reward = 0; // 等級5獎勵編號
	public int Lv6Reward = 0; // 等級6獎勵編號

	// 取得條件值
	public int GetValue(int iLevel)
	{
		switch(iLevel)
		{
		case 1: return Lv1Value;
		case 2: return Lv2Value;
		case 3: return Lv3Value;
		case 4: return Lv4Value;
		case 5: return Lv5Value;
		case 6: return Lv6Value;
		default: return 0;
		}//switch
	}
	// 取得獎勵編號
	public int GetReward(int iLevel)
	{
		switch(iLevel)
		{
		case 1: return Lv1Reward;
		case 2: return Lv2Reward;
		case 3: return Lv3Reward;
		case 4: return Lv4Reward;
		case 5: return Lv5Reward;
		case 6: return Lv6Reward;
		default: return 0;
		}//switch
	}
}
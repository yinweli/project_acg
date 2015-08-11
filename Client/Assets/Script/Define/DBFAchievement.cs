using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFAchievement : DBF
{
	/* 
	 * 成就DBF的GUID就是ENUM_Achievement的編號
	 */

	public int Name = 0; // 成就名稱
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
}
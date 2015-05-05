using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFEquip : DBF
{
	public int StrID = 0; // 字串編號
	public int Mode = 0; // 模式
	public int Threat = 0; // 威脅
	public int Gain = 0; // 獲得機率
	public int Damage = 0; // 傷害
	public float CriticalStrike = 0; // 致命
	public float FireRate = 0.0f; // 射速
	public int Range = 0; // 射程
	public int Resource = 0; // 資源
}
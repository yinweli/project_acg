using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFFeature : DBF
{
	public int StrID = 0; // 字串編號
	public int Mode = 0; // 模式
	public int Group = 0; // 群組
	public float Chances = 0; // 機率
	public float Cooldown = 0; // 冷卻時間
	public int Effect = 0; // 效果
	public string Value = ""; // 效果值
}
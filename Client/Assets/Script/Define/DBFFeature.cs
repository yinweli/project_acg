using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFFeature : DBF
{
	public int StrID = 0; // 字串編號
	public int Mode = 0; // 模式
	public int Gain = 0; // 獲得機率
	public int Group = 0; // 群組
	public string Value = ""; // 效果值
	public int Description = 0; // 描述字串編號
}
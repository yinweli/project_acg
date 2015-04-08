using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class DBFMonster : DBF
{
	public int Mode = 0; // 模式
	public int StageID = 0; // 關卡編號
	public int HP = 0; // HP
	public float MoveSpeed = 0; // 移動速度
	public int Threat = 0; // 威脅
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInit
{
	public const int iInitCurrency = 0; // 初始金錢
	public const int iInitBattery = 300; // 初始電池值
	public const int iInitLightAmmo = 120; // 初始輕型彈藥值
	public const int iInitHeavyAmmo = 100; // 初始重型彈藥值
	public const int iInitBomb = 3; // 初始炸彈數量

	public static List<int> InitMemberLooks = new List<int>() // 初始成員外觀列表
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 24, 28, 29, 31, 32, 
	};
}
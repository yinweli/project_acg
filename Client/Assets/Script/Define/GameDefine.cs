using System;
using System.Collections;
using System.Collections.Generic;

public class GameDefine
{
    public const float fMoveSpeed = 50f; // 玩家移動速度.
	public const int iMaxCurrency = 9999; // 最大通貨值
	public const int iMaxPressure = 100; // 最大壓力值
	public const int iMaxBattery = 9999; // 最大電池值
	public const int iMaxLightAmmo = 9999; // 最大輕型彈藥值
	public const int iMaxHeavyAmmo = 9999; // 最大重型彈藥值
	public const int iStaminaLimit = 100; // 基礎耐力上限值
	public const int iStaminaRecovery = 10; // 基礎耐力回復值
	
    public const int iWeightEngry = 2;  // 怪物出怪能量關卡加權.
    public const int iBaseEngry = 100;  // 怪物出怪能量基礎值.
    public const int iMINWaitSec = 5;   // 怪物出怪最小間隔秒數.
    public const int iMAXWaitSec = 21;  // 怪物出怪最大間隔秒數.

	public const int iBlockSize = 64; // 格子尺寸
	public const int iBlockUpdate = 20; // 格子更新距離
	public const int iStageLevel = 5; // 關卡升階值
	public const int iRoadSizeBase = 500; // 地圖道路基礎長度
	public const int iRoadSizeAdd = 10; // 地圖道路增加長度
	public const int iMapWidth = 21; // 地圖寬度
	public const int iMapBorderX = 4; // 地圖X軸邊框長度
	public const int iMapBorderY = 6; // 地圖Y軸邊框長度
	public const int iPathStart = 10; // 起點路徑長度
	public const int iPathMin = 4; // 最小路徑長度
	public const int iPathMax = 12; // 最長路徑長度
	public const int iObjtProb = 50; // 物件出現機率
	public const int iObjtDec = 8; // 物件遞減機率

	public const string szSave = "save"; // 存檔名稱

	public const string szDBFEquip = "Equip"; // 裝備dbf名稱
	public const string szDBFFeature = "Feature"; // 特性dbf名稱
	public const string szDBFLanguage = "Language"; // 語言dbf名稱
	public const string szDBFMonster = "Monster"; // 怪物dbf名稱

	public static readonly List<MapCoor> ObjtScale = new List<MapCoor>() // 物件尺寸列表
	{
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(2, 1), // 物件(2x1)
		new MapCoor(2, 1), // 物件(2x1)
		new MapCoor(1, 2), // 物件(1x2)
		new MapCoor(1, 2), // 物件(1x2)
		new MapCoor(2, 2), // 物件(2x2)
	};
}

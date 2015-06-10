﻿using System;
using System.Collections;
using System.Collections.Generic;

public class GameDefine
{
    public const int iSaveSec = 2; // 存檔秒數.

	public const int iInitBattery = 300; // 初始電池值
	public const int iInitLightAmmo = 120; // 初始輕型彈藥值
	public const int iInitHeavyAmmo = 100; // 初始重型彈藥值
	public const int iInitBomb = 3; // 初始炸彈數量

	public const int iPriceBattery = 1; // 電池價格
	public const int iPriceLightAmmo = 3; // 輕型彈藥價格
	public const int iPriceHeavyAmmo = 3; // 重型彈藥價格

    public const int iBatteryCount = 30; // 電池值購買一單位數量.
	public const int iBatteryCost = iPriceBattery * iBatteryCount; // 電池值價格.
	public const int iLightAmmoCount = 30; // 輕型彈藥購買一單位數量.
	public const int iLightAmmoCost = iPriceLightAmmo * iLightAmmoCount; // 輕型彈藥價格.
    public const int iHeavyAmmoCount = 30; // 重型彈藥購買一單位數量.
	public const int iHeavyAmmoCost = iPriceHeavyAmmo * iHeavyAmmoCount; // 重型彈藥價格.

    public const int iBatteryTimeCost = -1; // 電池每跳每次扣除額.
    public const float fBatteryTime = 0.5f; // 電池每跳時間.

    public const float fBaseSpeed = 64f; // 玩家移動速度.
	public const float fCriticalStrikProb = 1.0f; // 致命計算機率
	public const float fCriticalStrik = 1.5f; // 致命傷害倍數
	public const int iMaxMember = 6; // 最大成員數
	public const int iMaxSex = 2; // 最大性別數
	public const int iMaxLook = 15; // 最大外觀數
	public const int iMaxCurrency = 9999; // 最大通貨值
	public const int iMaxBattery = 300; // 最大電池值
	public const int iMaxStamina = 9999; // 最大耐力值
	public const int iMinStamina = 10; // 耐力下限值
	public const int iMaxStaminaRecovery = 10; // 最大耐力回復值
	public const int iMaxLightAmmo = 9999; // 最大輕型彈藥值
	public const int iMaxHeavyAmmo = 9999; // 最大重型彈藥值
	public const int iMaxFeature = 6; // 最大特性數量
	public const int iMaxBomb = 10; // 最大絕招數量
	public const int iMaxShield = 100; // 最大護盾值
	public const int iBaseStaminaLimit = 30; // 基礎耐力上限值
	public const int iBaseStaminaRecovery = 5; // 基礎耐力回復值
	public const int iStaminaConsume = 5; // 耐力消耗值
	public const int iStaminaRecoveryTime = 5; // 耐力回復時間
	public const int iStaminaConsumeTime = 1; // 耐力消耗時間
	public const int iDamageUpgrade = 1; // 升級傷害值
	public const int iDamageClick = 3; // 點擊傷害值
	public const int iDamageBomb = 200; // 絕招傷害值
	public const int iDamageShield = 200; // 護盾傷害值
	
    public const int iBaseEngry = 3;  // 怪物出怪能量基礎值.
	public const int iMaxWaitSec = 12; // 怪物出怪最大間隔秒數.
	public const int iMinWaitSec = 8; // 怪物出怪最小間隔秒數.

	public const int iBlockSize = 64; // 格子尺寸
	public const int iBlockUpdate = 25; // 格子更新距離
	public const int iRoadSizeBase = 100; // 地圖道路基礎長度
	public const int iMapWidth = 41; // 地圖寬度
	public const int iMapBorderX = 12; // 地圖X軸邊框長度
	public const int iMapBorderY = 12; // 地圖Y軸邊框長度
	public const int iPathStart = 12; // 起點路徑長度
	public const int iPathMin = 4; // 最小路徑長度
	public const int iPathMax = 12; // 最長路徑長度
	public const int iObjtProb = 50; // 物件出現機率
	public const int iObjtDec = 2; // 物件遞減機率

	public const int iAdsMoneyBase = 50; // 廣告影片贈送金錢基礎值
	public const int iAdsMoneyStage = 25; // 廣告影片贈送金錢增加值
	public const int iPickupSearch = 100; // 拾取物品建立嘗試次數
	public const int iPickupBorder = 10; // 拾取物品出現邊界
	public const int iPickupMember = 25; // 救援成員出現機率
	public const int iMaxPickupItems = 24; // 最大拾取物品次數
	public const int iMinPickupItems = 20; // 最小拾取物品次數
	public const int iMaxPickupValue = 24; // 最大拾取物品價值
	public const int iMinPickupValue = 20; // 最小拾取物品價值
	public const int iPickupProbBomb = 10; // 拾取物品(絕招)出現機率
	public const float fPickupPartLightAmmo = 0.15f; // 拾取物品(輕型彈藥)比例
	public const float fPickupPartHeavyAmmo = 0.15f; // 拾取物品(重型彈藥)比例
	public const float fPickupPartBattery = 0.2f; // 拾取物品(電池)比例

	public const int iEquipExtra = 20; // 額外裝備機率值

	public const float fUpgradeRoad = 1.5f; // 地圖道路升級值
	public const float fUpgradeEnegry = 1.5f; // 怪物能量升級值
	public const float fUpgradePickup = 2.0f; // 拾取物品價值升級值

	public const string szSaveMusic = "save_music"; // 音樂開關存檔
	public const string szSaveSound = "save_sound"; // 音效開關存檔
	public const string szSavePlayer = "save_player"; // 玩家資料存檔名稱
	public const string szSaveGame = "save_game"; // 遊戲資料存檔名稱
	public const string szSaveMap = "save_map"; // 地圖資料存檔名稱
	public const string szSaveEnemyCount = "save_enemy_count"; // 怪物資料數量存檔名稱
	public const string szSaveEnemy = "save_enemy_"; // 怪物資料存檔名稱
	public const string szSaveRecordCount = "save_record_count"; // 遊戲紀錄數量存檔名稱
	public const string szSaveRecord = "save_reocrd_"; // 遊戲紀錄存檔名稱

	public const string szDBFEquip = "Equip"; // 裝備dbf名稱
	public const string szDBFFeature = "Feature"; // 特性dbf名稱
	public const string szDBFLanguage = "Language"; // 語言dbf名稱
	public const string szDBFMonster = "Monster"; // 怪物dbf名稱

	public static readonly List<int> StageStyle = new List<int>() // 關卡風格列表
	{
		0, 1, 2, 3, 
	};
	public static readonly List<int> PickupRange = new List<int>() // 拾取範圍列表
	{
		-3, -2, -1, 1, 2, 3, 
	};
	public static readonly List<MapCoor> ObjtScale = new List<MapCoor>() // 物件尺寸列表
	{
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(1, 1), // 物件(1x1)
		new MapCoor(2, 1), // 物件(2x1)
		new MapCoor(2, 1), // 物件(2x1)
		new MapCoor(2, 1), // 物件(2x1)
		new MapCoor(1, 2), // 物件(1x2)
		new MapCoor(1, 2), // 物件(1x2)
		new MapCoor(1, 2), // 物件(1x2)
		new MapCoor(2, 2), // 物件(2x2)
		new MapCoor(2, 2), // 物件(2x2)
	};
	public static readonly MapCoor ObjtStart = new MapCoor(1, 1); // 起點物件尺寸
	public static readonly MapCoor ObjtEnd = new MapCoor(3, 2); // 終點物件尺寸
	public static readonly MapCoor ObjtBase = new MapCoor(1, 1); // 底圖物件尺寸
}

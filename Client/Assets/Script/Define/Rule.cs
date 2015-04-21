﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Rule
{
	// 取得值結果
	public static int Value(int iMin, int iMax, int iValue)
	{
		return Mathf.Max(iMin, Mathf.Min(iMax, iValue));
	}
	// 取得值結果
	public static float Value(float fMin, float fMax, float fValue)
	{
		return Mathf.Max(fMin, Mathf.Min(fMax, fValue));
	}
	// 取得特性效果值
	public static int FeatureI(ENUM_ModeFeature emMode)
	{
		int iResult = 0;

		for(int iPos = 0; iPos < PlayerData.pthis.Members.Count; ++iPos)
			iResult += FeatureI(emMode, iPos);
		
		return iResult;
	}
	// 取得特性效果值
	public static int FeatureI(ENUM_ModeFeature emMode, int iPos)
	{
		int iResult = 0;

		if(PlayerData.pthis.Members.Count > iPos)
		{
			foreach(int Itor in PlayerData.pthis.Members[iPos].Feature)
			{
				DBFFeature Data = GameDBF.This.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)emMode)
					iResult += System.Convert.ToInt32(Data.Value);
			}//for
		}//if
		
		return iResult;
	}
	// 取得特性效果值
	public static float FeatureF(ENUM_ModeFeature emMode)
	{
		float fResult = 0.0f;
		
		for(int iPos = 0; iPos < PlayerData.pthis.Members.Count; ++iPos)
			fResult += FeatureF(emMode, iPos);
		
		return fResult;
	}
	// 取得特性效果值
	public static float FeatureF(ENUM_ModeFeature emMode, int iPos)
	{
		float fResult = 0;
		
		if(PlayerData.pthis.Members.Count > iPos)
		{
			foreach(int Itor in PlayerData.pthis.Members[iPos].Feature)
			{
				DBFFeature Data = GameDBF.This.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)emMode)
					fResult += System.Convert.ToSingle(Data.Value);
			}//for
		}//if
		
		return fResult;
	}
	// 重置通貨值
	public static void CurrencyReset()
	{
		PlayerData.pthis.iCurrency = Value(0, GameDefine.iMaxCurrency, 0);
	}
	// 增加通貨值
	public static void CurrencyAdd(int iValue)
	{
		PlayerData.pthis.iCurrency = Value(0, GameDefine.iMaxCurrency, PlayerData.pthis.iCurrency + iValue);
	}
	// 重置耐力值
	public static void StaminaReset()
	{
		PlayerData.pthis.iStamina = Value(0, GameDefine.iMaxStamina, PlayerData.pthis.iStaminaLimit);
	}
	// 增加耐力值
	public static void StaminaAdd(int iValue)
	{
		PlayerData.pthis.iStamina = Value(0, PlayerData.pthis.iStaminaLimit, PlayerData.pthis.iStamina + iValue);
	}
	// 重置耐力上限值
	public static int StaminaLimit()
	{
		return Value(0, GameDefine.iMaxStamina, FeatureI(ENUM_ModeFeature.Passive_StaminaLimit) + GameDefine.iBaseStaminaLimit);
	}
	// 重置耐力回復值
	public static void StaminaRecovery()
	{
		PlayerData.pthis.iStaminaRecovery = Value(0, GameDefine.iMaxStaminaRecovery, FeatureI(ENUM_ModeFeature.Passive_StaminaRecovery) + GameDefine.iBaseStaminaRecovery);
	}
	// 重置資源
	public static void ResourceReset(ENUM_Resource emResource)
	{
		while(PlayerData.pthis.Resource.Count <= (int)emResource)
			PlayerData.pthis.Resource.Add(0);

		PlayerData.pthis.Resource[(int)emResource] = 0;
	}
	// 增加資源
	public static void ResourceAdd(ENUM_Resource emResource, int iValue)
	{
		while(PlayerData.pthis.Resource.Count <= (int)emResource)
			PlayerData.pthis.Resource.Add(0);

		int iResult = PlayerData.pthis.Resource[(int)emResource];

		switch(emResource)
		{
		case ENUM_Resource.Battery: iResult = Value(0, System.Int32.MaxValue, iResult + iValue); break;
		case ENUM_Resource.LightAmmo: iResult = Value(0, GameDefine.iMaxLightAmmo, iResult + iValue); break;
		case ENUM_Resource.HeavyAmmo: iResult = Value(0, GameDefine.iMaxHeavyAmmo, iResult + iValue); break;
		default: break;
		}//switch

		PlayerData.pthis.Resource[(int)emResource] = iResult;
	}
	// 檢查是否資源足夠
	public static bool ResourceChk(ENUM_Resource emResource, int iValue)
	{
		if(emResource == ENUM_Resource.Null)
			return true;

		int iIndex = (int)emResource;

		if(PlayerData.pthis.Resource.Count <= iIndex)
			return false;

		return PlayerData.pthis.Resource[iIndex] >= iValue;
	}
	// 建立成員
	public static void MemberAdd(int iSex, int iLook, int iEquip)
	{
		Member MemberTemp = new Member();
		
		MemberTemp.iSex = iSex;
		MemberTemp.iLook = iLook;
		MemberTemp.iEquip = iEquip;

		PlayerData.pthis.Members.Add(MemberTemp);
	}
	// 建立成員
	public static void MemberAdd(int iEquip)
	{
		MemberAdd(Random.Range(0, GameDefine.iMaxSex), Random.Range(0, GameDefine.iMaxLook), iEquip);
	}
	// 建立成員
	public static void MemberAdd()
	{
		MemberAdd(0);
	}
	// 刪除成員
	public void MemberDel(int iPos)
	{
		PlayerData.pthis.Members.RemoveAt(iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset(int iPos)
	{
		if(PlayerData.pthis.Members.Count > iPos)
			PlayerData.pthis.Members[iPos].fCriticalStrike = FeatureF(ENUM_ModeFeature.Passive_CriticalStrike, iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset()
	{
		for(int iPos = 0; iPos < PlayerData.pthis.Members.Count; ++iPos)
			CriticalStrikeReset(iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset(int iPos)
	{
		if(PlayerData.pthis.Members.Count > iPos)
			PlayerData.pthis.Members[iPos].iAddDamage = FeatureI(ENUM_ModeFeature.Passive_AddDamage, iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset()
	{
		for(int iPos = 0; iPos < PlayerData.pthis.Members.Count; ++iPos)
			AddDamageReset(iPos);
	}
	// 依方向取得座標列表
	public static List<MapCoor> NextPath(ENUM_Dir emDir, MapCoor Pos)
	{
		List<MapCoor> Result = new List<MapCoor>();
		int iRoadWidthMin = GameDefine.iMapBorderX;
		int iRoadWidthMax = GameDefine.iMapWidth - GameDefine.iMapBorderX;
		int iLength = Pos == null ? GameDefine.iPathStart : Random.Range(GameDefine.iPathMin, GameDefine.iPathMax);
		
		while(iLength > 0)
		{
			if(Pos == null)
				Pos = new MapCoor(Random.Range(iRoadWidthMin, iRoadWidthMax), GameDefine.iMapBorderY);
			else
				Pos = Pos.Add(emDir, 1);
			
			if(Pos.X >= iRoadWidthMin && Pos.X < iRoadWidthMax && Pos.Y >= 1)
			{
				Result.Add(Pos);
				--iLength;
			}
			else
				iLength = 0;
		}//while
		
		return Result;
	}
	// 取得隨機物件
	public static Tuple<ENUM_Map, MapCoor> NextObjt()
	{
		int iIndex = Random.Range(0, GameDefine.ObjtScale.Count);
		
		return new Tuple<ENUM_Map, MapCoor>((ENUM_Map)(iIndex + ENUM_Map.MapObjt_0), GameDefine.ObjtScale[iIndex]);
	}
	// 取得隨機地圖拾取位置
	public static MapCoor NextPickup()
	{
		if(GameData.pthis.RoadList.Count <= GameDefine.iPickupBorder)
			return new MapCoor();

		MapCoor Road = GameData.pthis.RoadList[Random.Range(GameDefine.iPickupBorder, GameData.pthis.RoadList.Count - GameDefine.iPickupBorder)];

		for(int iCount = 0; iCount < GameDefine.iPickupSearch; ++iCount)
		{
			MapCoor Result = new MapCoor(Road.X + Tool.RandomPick(GameDefine.PickupRange), Road.Y + Tool.RandomPick(GameDefine.PickupRange));
			bool bCheck = true;

			foreach(MapCoor Itor in GameData.pthis.RoadList)
				bCheck &= (Result.X == Itor.X && Result.Y == Itor.Y) == false;

			foreach(Pickup Itor in GameData.pthis.PickupList)
				bCheck &= (Result.X == Itor.Pos.X && Result.Y == Itor.Pos.Y) == false;

			if(bCheck)
				return Result;
		}//for

		return new MapCoor();
	}
	// 執行獲得裝備
	public static int GainEquip(int iPos)
	{
		if(PlayerData.pthis.Members.Count <= iPos)
			return 0;
		
		if(PlayerData.pthis.Members[iPos].iEquip > 0)
			return 0;
		
		// 檢查隊伍裡是否沒有光源裝備
		bool bLight = false;
		
		foreach(Member ItorMember in PlayerData.pthis.Members)
		{
			DBFEquip Data = GameDBF.This.GetEquip(ItorMember.iEquip) as DBFEquip;
			
			if(Data != null && Data.Mode == (int)ENUM_ModeEquip.Light)
				bLight = true;
		}//for
		
		// 有光源裝備的話, 就照一般規則獲得裝備
		// 否則就一定拿到光源裝備
		if(bLight)
		{
			// 建立獲得裝備骰子
			int iProb = 100;
			CDice<int> Dice = new CDice<int>();
			DBFItor Itor = GameDBF.This.GetEquip();
			
			while(Itor.IsEnd() == false)
			{
				DBFEquip Data = Itor.Data() as DBFEquip;
				
				if(Data != null && Data.StageID <= PlayerData.pthis.iStage)
				{
					iProb -= Data.Gain;
					Dice.Set(System.Convert.ToInt32(Data.GUID), Data.Gain);
				}//if
				
				Itor.Next();
			}//while

			if(iProb > 0)
				Dice.Set(0, iProb); // 加入失敗項
			
			return PlayerData.pthis.Members[iPos].iEquip = Dice.Roll();
		}
		else
			return PlayerData.pthis.Members[iPos].iEquip = 1; // 1號是手電筒
	}
	// 執行獲得特性
	public static int GainFeature(int iPos)
	{
		if(PlayerData.pthis.Members.Count <= iPos)
			return 0;
		
		// 建立特性群組列表
		HashSet<int> Group = new HashSet<int>();
		
		foreach(int ItorFeature in PlayerData.pthis.Members[iPos].Feature)
		{
			DBFFeature Data = GameDBF.This.GetFeature(ItorFeature) as DBFFeature;
			
			if(Data != null)
				Group.Add(Data.Group);
		}//for
		
		// 建立獲得特性骰子
		int iProb = 100;
		CDice<int> Dice = new CDice<int>();
		DBFItor Itor = GameDBF.This.GetFeature();
		
		while(Itor.IsEnd() == false)
		{
			DBFFeature Data = Itor.Data() as DBFFeature;
			
			if(Data != null && Data.StageID <= PlayerData.pthis.iStage && Group.Contains(Data.Group) == false)
			{
				iProb -= Data.Gain;
				Dice.Set(System.Convert.ToInt32(Data.GUID), Data.Gain);
			}//if
			
			Itor.Next();
		}//while

		if(iProb > 0)
			Dice.Set(0, iProb); // 加入失敗項
		
		int iFeature = Dice.Roll();
		
		if(iFeature > 0)
			PlayerData.pthis.Members[iPos].Feature.Add(iFeature);
		
		return iFeature;
	}
	// 取得子彈傷害值
	public static int BulletDamage(int iPos)
	{
		int iResult = 0;

		if(PlayerData.pthis.Members.Count > iPos)
		{
			Member DataMember = PlayerData.pthis.Members[iPos];
			DBFEquip DataEquip = GameDBF.This.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;

			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
			{
				if(Random.Range(0.0f, GameDefine.fCriticalStrikProb) > (DataMember.fCriticalStrike + DataEquip.CriticalStrike))
					iResult = (DataMember.iAddDamage + DataEquip.Damage);
				else
					iResult = (int)((DataMember.iAddDamage + DataEquip.Damage) * GameDefine.fCriticalStrik);
			}//if
		}//if

		return iResult;
	}
	// 取得裝備攻擊間隔時間
	public static float EquipFireRate(int iPos)
	{
		float fResult = 0;
		
		if(PlayerData.pthis.Members.Count > iPos)
		{
			Member DataMember = PlayerData.pthis.Members[iPos];
			DBFEquip DataEquip = GameDBF.This.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;
			
			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
				fResult = DataEquip.FireRate;
		}//if
		
		return fResult;
	}
	// 取得可產生怪物列表
	public static List<int> MonsterList()
	{
		List<int> Result = new List<int>();
		DBFItor Itor = GameDBF.This.GetMonster();

		while(Itor.IsEnd() == false)
		{
			DBFMonster Data = Itor.Data() as DBFMonster;

			if(Data != null && Data.StageID <= PlayerData.pthis.iStage)
				Result.Add(System.Convert.ToInt32(Data.GUID));

			Itor.Next();
		}//while

		return Result;
	}
}
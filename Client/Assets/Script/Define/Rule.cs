using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Rule
{
	// 取得值結果
	public static int ValueI(int iMin, int iMax, int iValue)
	{
		return Mathf.Max(iMin, Mathf.Min(iMax, iValue));
	}
	// 取得值結果
	public static float ValueF(float fMin, float fMax, float fValue)
	{
		return Mathf.Max(fMin, Mathf.Min(fMax, fValue));
	}
	// 取得特性被動效果值
	public static int PassiveI(ENUM_Effect emEffect)
	{
		int iResult = 0;

		for(int iPos = 0; iPos < PlayerData.pthis.Data.Count; ++iPos)
			iResult += PassiveI(emEffect, iPos);
		
		return Mathf.Max(0, iResult);
	}
	// 取得特性被動效果值
	public static int PassiveI(ENUM_Effect emEffect, int iPos)
	{
		int iResult = 0;

		if(PlayerData.pthis.Data.Count > iPos)
		{
			foreach(int Itor in PlayerData.pthis.Data[iPos].Feature)
			{
				DBFFeature Data = GameDBF.This.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)ENUM_ModeFeature.Passive && Data.Effect == (int)emEffect)
					iResult += System.Convert.ToInt32(Data.Value);
			}//for
		}//if
		
		return Mathf.Max(0, iResult);
	}
	// 取得特性被動效果值
	public static float PassiveF(ENUM_Effect emEffect, int iPos)
	{
		float fResult = 0;
		
		if(PlayerData.pthis.Data.Count > iPos)
		{
			foreach(int Itor in PlayerData.pthis.Data[iPos].Feature)
			{
				DBFFeature Data = GameDBF.This.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)ENUM_ModeFeature.Passive && Data.Effect == (int)emEffect)
					fResult += System.Convert.ToSingle(Data.Value);
			}//for
		}//if
		
		return Mathf.Max(0.0f, fResult);
	}
	// 取得特性被動效果值
	public static float PassiveF(ENUM_Effect emEffect)
	{
		float fResult = 0.0f;

		for(int iPos = 0; iPos < PlayerData.pthis.Data.Count; ++iPos)
			fResult += PassiveF(emEffect, iPos);

		return Mathf.Max(0.0f, fResult);
	}
	// 重置通貨值
	public static void CurrencyReset()
	{
		PlayerData.pthis.iCurrency = PassiveI(ENUM_Effect.Currency);
	}
	// 增加通貨值
	public static void CurrencyAdd(int iValue)
	{
		PlayerData.pthis.iCurrency = ValueI(PassiveI(ENUM_Effect.Currency), GameDefine.iMaxCurrency, PlayerData.pthis.iCurrency + iValue);
	}
	// 重置壓力值
	public static void PressureReset()
	{
		PlayerData.pthis.iPressure = PassiveI(ENUM_Effect.Pressure);
	}
	// 增加壓力值
	public static void PressureAdd(int iValue)
	{
		PlayerData.pthis.iPressure = ValueI(PassiveI(ENUM_Effect.Pressure), GameDefine.iMaxPressure, PlayerData.pthis.iPressure + iValue);
	}
	// 重置耐力值
	public static void StaminaReset()
	{
		PlayerData.pthis.iStamina = PassiveI(ENUM_Effect.Stamina) + PlayerData.pthis.iStaminaLimit;
	}
	// 增加耐力值
	public static void StaminaAdd(int iValue)
	{
		PlayerData.pthis.iStamina = ValueI(PassiveI(ENUM_Effect.Stamina), PlayerData.pthis.iStaminaLimit, PlayerData.pthis.iStamina + iValue);
	}
	// 重置耐力上限值
	public static void StaminaLimit()
	{
		PlayerData.pthis.iStaminaLimit = PassiveI(ENUM_Effect.StaminaLimit) + GameDefine.iStaminaLimit;
	}
	// 重置耐力回復值
	public static void StaminaRecovery()
	{
		PlayerData.pthis.iStaminaRecovery = PassiveI(ENUM_Effect.StaminaRecovery) + GameDefine.iStaminaRecovery;
	}
	// 重置資源
	public static void ResourceReset(ENUM_Resource emResource)
	{
		int iIndex = 0;

		while(iIndex <= (int)emResource)
			PlayerData.pthis.Resource.Add(0);

		PlayerData.pthis.Resource[(int)emResource] = emResource == ENUM_Resource.Battery ? PassiveI(ENUM_Effect.Battery) : 0;
	}
	// 增加資源
	public static void ResourceAdd(ENUM_Resource emResource, int iValue)
	{
		int iIndex = 0;
		
		while(iIndex++ <= (int)emResource)
			PlayerData.pthis.Resource.Add(0);

		int iResult = PlayerData.pthis.Resource[(int)emResource];

		switch(emResource)
		{
		case ENUM_Resource.Battery: iResult = ValueI(PassiveI(ENUM_Effect.Battery), GameDefine.iMaxBattery, iResult + iValue); break;
		case ENUM_Resource.LightAmmo: iResult = ValueI(0, GameDefine.iMaxLightAmmo, iResult + iValue); break;
		case ENUM_Resource.HeavyAmmo: iResult = ValueI(0, GameDefine.iMaxHeavyAmmo, iResult + iValue); break;
		default: break;
		}//switch

		PlayerData.pthis.Resource[(int)emResource] = iResult;
	}
	// 檢查是否資源足夠
	public static bool ResourceChk(ENUM_Resource emResource, int iValue)
	{
		int iIndex = (int)emResource;

		return PlayerData.pthis.Resource.Count > iIndex ? PlayerData.pthis.Resource[iIndex] >= iValue : false;
	}
	// 建立成員
	public static void MemberAdd(Looks Looks, int iEquip)
	{
		Member MemberTemp = new Member();
		
		MemberTemp.Looks = Looks;
		MemberTemp.iEquip = iEquip;

		PlayerData.pthis.Data.Add(MemberTemp);
	}
	// 刪除成員
	public void MemberDel(int iPos)
	{
		PlayerData.pthis.Data.RemoveAt(iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset(int iPos)
	{
		if(PlayerData.pthis.Data.Count > iPos)
			PlayerData.pthis.Data[iPos].fCriticalStrike = PassiveF(ENUM_Effect.CriticalStrike, iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset()
	{
		for(int iPos = 0; iPos < PlayerData.pthis.Data.Count; ++iPos)
			CriticalStrikeReset(iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset(int iPos)
	{
		if(PlayerData.pthis.Data.Count > iPos)
			PlayerData.pthis.Data[iPos].iAddDamage = PassiveI(ENUM_Effect.AddDamage, iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset()
	{
		for(int iPos = 0; iPos < PlayerData.pthis.Data.Count; ++iPos)
			AddDamageReset(iPos);
	}
	// 取得子彈傷害值
	public static int BulletDamage(int iPos)
	{
		int iResult = 0;

		if(PlayerData.pthis.Data.Count > iPos)
		{
			Member DataMember = PlayerData.pthis.Data[iPos];
			DBFEquip DataEquip = GameDBF.This.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;

			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
			{
				if(Random.Range(0.0f, GameDefine.fCriticalStrikProb) > (DataMember.fCriticalStrike + DataEquip.CriticalStrike))
					iResult = (DataMember.iAddDamage + DataEquip.Damage);
				else
					iResult = (DataMember.iAddDamage + DataEquip.Damage) * GameDefine.iCriticalStrik;
			}//if
		}//if

		return iResult;
	}
	// 取得裝備攻擊間隔時間
	public static float EquipFireRate(int iPos)
	{
		float fResult = 0;
		
		if(PlayerData.pthis.Data.Count > iPos)
		{
			Member DataMember = PlayerData.pthis.Data[iPos];
			DBFEquip DataEquip = GameDBF.This.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;
			
			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
				fResult = DataEquip.FireRate;
		}//if
		
		return fResult;
	}
	// 取得可產生怪物列表
	public static List<int> MonsterList(int iStage)
	{
		List<int> Result = new List<int>();
		DBFItor Itor = GameDBF.This.GetMonster();

		while(Itor.IsEnd() == false)
		{
			DBFMonster Data = Itor.Data() as DBFMonster;

			if(Data != null && Data.StageID <= iStage)
				Result.Add(System.Convert.ToInt32(Data.GUID));

			Itor.Next();
		}//while

		return Result;
	}
	// 取得可獲得裝備列表
	public static List<int> EquipList(int iStage)
	{
		List<int> Result = new List<int>();
		DBFItor Itor = GameDBF.This.GetEquip();
		
		while(Itor.IsEnd() == false)
		{
			DBFEquip Data = Itor.Data() as DBFEquip;
			
			if(Data != null && Data.StageID <= iStage)
				Result.Add(System.Convert.ToInt32(Data.GUID));
			
			Itor.Next();
		}//while
		
		return Result;
	}
	// 取得可獲得特性列表
	public static List<int> FeatureList(int iStage)
	{
		List<int> Result = new List<int>();
		DBFItor Itor = GameDBF.This.GetEquip();
		
		while(Itor.IsEnd() == false)
		{
			DBFFeature Data = Itor.Data() as DBFFeature;
			
			if(Data != null && Data.StageID <= iStage)
				Result.Add(System.Convert.ToInt32(Data.GUID));
			
			Itor.Next();
		}//while
		
		return Result;
	}
}
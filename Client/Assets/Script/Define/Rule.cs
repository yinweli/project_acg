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

		for(int iPos = 0; iPos < SysMain.pthis.Data.Data.Count; ++iPos)
			iResult += PassiveI(emEffect, iPos);
		
		return Mathf.Max(0, iResult);
	}
	// 取得特性被動效果值
	public static int PassiveI(ENUM_Effect emEffect, int iPos)
	{
		int iResult = 0;

		if(SysMain.pthis.Data.Data.Count > iPos)
		{
			foreach(int Itor in SysMain.pthis.Data.Data[iPos].Feature)
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
		
		if(SysMain.pthis.Data.Data.Count > iPos)
		{
			foreach(int Itor in SysMain.pthis.Data.Data[iPos].Feature)
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

		for(int iPos = 0; iPos < SysMain.pthis.Data.Data.Count; ++iPos)
			fResult += PassiveF(emEffect, iPos);

		return Mathf.Max(0.0f, fResult);
	}
	// 重置通貨值
	public static void CurrencyReset()
	{
		SysMain.pthis.Data.iCurrency = PassiveI(ENUM_Effect.Currency);
	}
	// 增加通貨值
	public static void CurrencyAdd(int iValue)
	{
		SysMain.pthis.Data.iCurrency = ValueI(PassiveI(ENUM_Effect.Currency), GameDefine.iMaxCurrency, SysMain.pthis.Data.iCurrency + iValue);
	}
	// 重置壓力值
	public static void PressureReset()
	{
		SysMain.pthis.Data.iPressure = PassiveI(ENUM_Effect.Pressure);
	}
	// 增加壓力值
	public static void PressureAdd(int iValue)
	{
		SysMain.pthis.Data.iPressure = ValueI(PassiveI(ENUM_Effect.Pressure), GameDefine.iMaxPressure, SysMain.pthis.Data.iPressure + iValue);
	}
	// 重置耐力值
	public static void StaminaReset()
	{
		SysMain.pthis.Data.iStamina = PassiveI(ENUM_Effect.Stamina);
	}
	// 增加耐力值
	public static void StaminaAdd(int iValue)
	{
		SysMain.pthis.Data.iStamina = ValueI(PassiveI(ENUM_Effect.Stamina), StaminaLimit(), SysMain.pthis.Data.iStamina + iValue);
	}
	// 取得耐力上限值
	public static int StaminaLimit()
	{
		return PassiveI(ENUM_Effect.StaminaLimit) + GameDefine.iStaminaLimit;
	}
	// 取得耐力回復值
	public static int StaminaRecovery()
	{
		return PassiveI(ENUM_Effect.StaminaRecovery) + GameDefine.iStaminaRecovery;
	}
	// 重置資源
	public static void ResourceReset(ENUM_Resource emResource)
	{
		int iIndex = 0;

		while(iIndex <= (int)emResource)
			SysMain.pthis.Data.Resource.Add(0);

		SysMain.pthis.Data.Resource[(int)emResource] = emResource == ENUM_Resource.Battery ? PassiveI(ENUM_Effect.Battery) : 0;
	}
	// 增加資源
	public static void ResourceAdd(ENUM_Resource emResource, int iValue)
	{
		int iIndex = 0;
		
		while(iIndex++ <= (int)emResource)
			SysMain.pthis.Data.Resource.Add(0);

		int iResult = SysMain.pthis.Data.Resource[(int)emResource];

		switch(emResource)
		{
		case ENUM_Resource.Battery: iResult = ValueI(PassiveI(ENUM_Effect.Battery), GameDefine.iMaxBattery, iResult + iValue); break;
		case ENUM_Resource.LightAmmo: iResult = ValueI(0, GameDefine.iMaxLightAmmo, iResult + iValue); break;
		case ENUM_Resource.HeavyAmmo: iResult = ValueI(0, GameDefine.iMaxHeavyAmmo, iResult + iValue); break;
		default: break;
		}//switch

		SysMain.pthis.Data.Resource[(int)emResource] = iResult;
	}
	// 檢查是否資源足夠
	public static bool ResourceChk(ENUM_Resource emResource, int iValue)
	{
		int iIndex = (int)emResource;

		return SysMain.pthis.Data.Resource.Count > iIndex ? SysMain.pthis.Data.Resource[iIndex] >= iValue : false;
	}
	// 重置成員致命值
	public static void CriticalStrikeReset(int iPos)
	{
		if(SysMain.pthis.Data.Data.Count > iPos)
			SysMain.pthis.Data.Data[iPos].fCriticalStrike = PassiveF(ENUM_Effect.CriticalStrike, iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset()
	{
		for(int iPos = 0; iPos < SysMain.pthis.Data.Data.Count; ++iPos)
			CriticalStrikeReset(iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset(int iPos)
	{
		if(SysMain.pthis.Data.Data.Count > iPos)
			SysMain.pthis.Data.Data[iPos].iAddDamage = PassiveI(ENUM_Effect.AddDamage, iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset()
	{
		for(int iPos = 0; iPos < SysMain.pthis.Data.Data.Count; ++iPos)
			AddDamageReset(iPos);
	}
	// 取得子彈傷害值
	public static int BulletDamage(int iPos)
	{
		int iResult = 0;

		if(SysMain.pthis.Data.Data.Count > iPos)
		{
			Member DataMember = SysMain.pthis.Data.Data[iPos];
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
		
		if(SysMain.pthis.Data.Data.Count > iPos)
		{
			Member DataMember = SysMain.pthis.Data.Data[iPos];
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
using UnityEngine;
using LibCSNStandard;
using System.Collections;

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
	// 重置電池值
	public static void BatteryReset()
	{
		SysMain.pthis.Data.iBattery = PassiveI(ENUM_Effect.Battery);
	}
	// 增加電池值
	public static void BatteryAdd(int iValue)
	{
		SysMain.pthis.Data.iBattery = ValueI(PassiveI(ENUM_Effect.Battery), GameDefine.iMaxBattery, SysMain.pthis.Data.iBattery + iValue);
	}
	// 重置輕型彈藥值
	public static void LightAmmoReset()
	{
		SysMain.pthis.Data.iLightAmmo = 0;
	}
	// 增加輕型彈藥值
	public static void LightAmmoAdd(int iValue)
	{
		SysMain.pthis.Data.iLightAmmo = ValueI(0, GameDefine.iMaxLightAmmo, SysMain.pthis.Data.iLightAmmo + iValue);
	}
	// 重置重型彈藥值
	public static void HeavyAmmoReset()
	{
		SysMain.pthis.Data.iHeavyAmmo = 0;
	}
	// 增加重型彈藥值
	public static void HeavyAmmoAdd(int iValue)
	{
		SysMain.pthis.Data.iHeavyAmmo = ValueI(0, GameDefine.iMaxHeavyAmmo, SysMain.pthis.Data.iHeavyAmmo + iValue);
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
}
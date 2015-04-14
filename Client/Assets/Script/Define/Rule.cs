using UnityEngine;
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
		case ENUM_Resource.Battery: iResult = Value(0, GameDefine.iMaxBattery, iResult + iValue); break;
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
	public static void MemberAdd(Looks Looks, int iEquip)
	{
		Member MemberTemp = new Member();
		
		MemberTemp.Looks = Looks;
		MemberTemp.iEquip = iEquip;

		PlayerData.pthis.Members.Add(MemberTemp);
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
	// 執行獲得裝備
	public static int GainEquip(int iPos)
	{
		if(PlayerData.pthis.Members.Count <= iPos)
			return 0;

		if(PlayerData.pthis.Members[iPos].iEquip > 0)
			return 0;

		// 建立獲得裝備骰子
		CDice<int> Dice = new CDice<int>();
		DBFItor Itor = GameDBF.This.GetEquip();
		
		while(Itor.IsEnd() == false)
		{
			DBFEquip Data = Itor.Data() as DBFEquip;
			
			if(Data != null && Data.StageID <= PlayerData.pthis.iStage)
				Dice.Set(System.Convert.ToInt32(Data.GUID), Data.Gain);
			
			Itor.Next();
		}//while

		Dice.Set(0, Dice.Max() * GameDefine.iGainDouble); // 加入失敗項

		return PlayerData.pthis.Members[iPos].iEquip = Dice.Roll();
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
		CDice<int> Dice = new CDice<int>();
		DBFItor Itor = GameDBF.This.GetFeature();
		
		while(Itor.IsEnd() == false)
		{
			DBFFeature Data = Itor.Data() as DBFFeature;
			
			if(Data != null && Data.StageID <= PlayerData.pthis.iStage && Group.Contains(Data.Group) == false)
				Dice.Set(System.Convert.ToInt32(Data.GUID), Data.Gain);
			
			Itor.Next();
		}//while
		
		Dice.Set(0, Dice.Max() * GameDefine.iGainDouble); // 加入失敗項

		int iFeature = Dice.Roll();

		if(iFeature > 0)
			PlayerData.pthis.Members[iPos].Feature.Add(iFeature);
		
		return iFeature;
	}
}
using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	// 取得是否有特性
	public static bool IsFeature(ENUM_ModeFeature emMode)
	{
		bool bResult = false;
		
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			bResult |= IsFeature(emMode, iPos);
		
		return bResult;
	}
	// 取得是否有特性
	public static bool IsFeature(ENUM_ModeFeature emMode, int iPos)
	{
		bool bResult = false;
		
		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			foreach(int Itor in DataPlayer.pthis.MemberParty[iPos].Feature)
			{
				DBFFeature Data = GameDBF.pthis.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)emMode)
					bResult = true;
			}//for
		}//if
		
		return bResult;
	}
	// 取得特性效果值
	public static int FeatureI(ENUM_ModeFeature emMode)
	{
		int iResult = 0;

		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			iResult += FeatureI(emMode, iPos);
		
		return iResult;
	}
	// 取得特性效果值
	public static int FeatureI(ENUM_ModeFeature emMode, int iPos)
	{
		int iResult = 0;

		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			foreach(int Itor in DataPlayer.pthis.MemberParty[iPos].Feature)
			{
				DBFFeature Data = GameDBF.pthis.GetFeature(new Argu(Itor)) as DBFFeature;
				
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
		
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			fResult += FeatureF(emMode, iPos);
		
		return fResult;
	}
	// 取得特性效果值
	public static float FeatureF(ENUM_ModeFeature emMode, int iPos)
	{
		float fResult = 0;
		
		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			foreach(int Itor in DataPlayer.pthis.MemberParty[iPos].Feature)
			{
				DBFFeature Data = GameDBF.pthis.GetFeature(new Argu(Itor)) as DBFFeature;
				
				if(Data != null && Data.Mode == (int)emMode)
					fResult += System.Convert.ToSingle(Data.Value);
			}//for
		}//if
		
		return fResult;
	}
	// 增加水晶值
	public static void CrystalAdd(int iValue)
	{
		DataReward.pthis.iCrystal = Value(0, GameDefine.iMaxCrystal, DataReward.pthis.iCrystal + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.Crystal, iValue);
	}
	// 重置通貨值
	public static void CurrencyReset()
	{
		DataPlayer.pthis.iCurrency = Value(0, GameDefine.iMaxCurrency, 0);
	}
	// 增加通貨值
	public static void CurrencyAdd(int iValue)
	{
		DataPlayer.pthis.iCurrency = Value(0, GameDefine.iMaxCurrency, DataPlayer.pthis.iCurrency + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.Currency, iValue);
	}
	// 重置電池
	public static void BatteryReset()
	{
		DataPlayer.pthis.iBattery = 0;
	}
	// 增加電池
	public static void BatteryAdd(int iValue)
	{
		DataPlayer.pthis.iBattery = Value(0, int.MaxValue, DataPlayer.pthis.iBattery + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.Battery, iValue);
	}
	// 檢查電池是否足夠
	public static bool BatteryChk(int iValue)
	{
		return DataPlayer.pthis.iBattery >= iValue;
	}
	// 重置輕型彈藥
	public static void LightAmmoReset()
	{
		DataPlayer.pthis.iLightAmmo = 0;
	}
	// 增加輕型彈藥
	public static void LightAmmoAdd(int iValue)
	{
		DataPlayer.pthis.iLightAmmo = Value(0, GameDefine.iMaxLightAmmo, DataPlayer.pthis.iLightAmmo + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.LightAmmo, iValue);
	}
	// 檢查輕型彈藥是否足夠
	public static bool LightAmmoChk(int iValue)
	{
		return DataPlayer.pthis.iLightAmmo >= iValue;
	}
	// 重置重型彈藥
	public static void HeavyAmmoReset()
	{
		DataPlayer.pthis.iHeavyAmmo = 0;
	}
	// 增加重型彈藥
	public static void HeavyAmmoAdd(int iValue)
	{
		DataPlayer.pthis.iHeavyAmmo = Value(0, GameDefine.iMaxHeavyAmmo, DataPlayer.pthis.iHeavyAmmo + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.HeavyAmmo, iValue);
	}
	// 檢查重型彈藥是否足夠
	public static bool HeavyAmmoChk(int iValue)
	{
		return DataPlayer.pthis.iHeavyAmmo >= iValue;
	}
	// 重置絕招次數
	public static void BombReset()
	{
		DataPlayer.pthis.iBomb = Value(0, GameDefine.iMaxBomb, Mathf.Max(DataPlayer.pthis.iBomb, FeatureI(ENUM_ModeFeature.AddLeastBomb)));
	}
	// 增加絕招次數
	public static void BombAdd(int iValue)
	{
		DataPlayer.pthis.iBomb = Value(0, GameDefine.iMaxBomb, DataPlayer.pthis.iBomb + iValue);
		Statistics.pthis.RecordResource(ENUM_Pickup.Bomb, iValue);
	}
	// 重置耐力值
	public static void StaminaReset()
	{
		DataPlayer.pthis.iStamina = Value(0, GameDefine.iMaxStamina, DataPlayer.pthis.iStaminaLimit);
	}
	// 增加耐力值
	public static void StaminaAdd(int iValue)
	{
		DataPlayer.pthis.iStamina = Value(0, DataPlayer.pthis.iStaminaLimit, DataPlayer.pthis.iStamina + iValue);
	}
	// 重置耐力上限值
	public static int StaminaLimit()
	{
		return Value(GameDefine.iMinStamina, GameDefine.iMaxStamina, FeatureI(ENUM_ModeFeature.StaminaLimit) + GameDefine.iBaseStaminaLimit);
	}
	// 重置耐力回復值
	public static void StaminaRecovery()
	{
		DataPlayer.pthis.iStaminaRecovery = Value(1, GameDefine.iMaxStaminaRecovery, FeatureI(ENUM_ModeFeature.StaminaRecovery) + GameDefine.iBaseStaminaRecovery);
	}
	// 重置護盾次數
	public static void ShieldReset()
	{
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			DataPlayer.pthis.MemberParty[iPos].iShield = Value(0, GameDefine.iMaxShield, FeatureI(ENUM_ModeFeature.AddShield, iPos));
	}
	// 取得隨機成員外型
	public static int RandomMemberLooks()
	{
		return Tool.RandomPick(DataReward.pthis.MemberLooks);
	}
	// 隊伍成員取得隨機裝備編號
	public static int RandomEquipParty(bool bMustLight, int iEquipExtra)
	{
		if(bMustLight)
			return 1; // 1號是手電筒

		// 建立獲得裝備骰子
		int iEquipProb = 0;
		int iEmptyProb = 100;
		CDice<int> Dice = new CDice<int>();
		DBFItor Itor = GameDBF.pthis.GetEquip();
		
		while(Itor.IsEnd() == false)
		{
			DBFEquip Temp = Itor.Data() as DBFEquip;
			
			if(Temp != null)
			{
				if(Temp.Mode == (int)ENUM_ModeEquip.Light)
					iEquipProb = Temp.Gain;
				else
					iEquipProb = Temp.Gain + iEquipExtra;
				
				iEmptyProb -= iEquipProb;
				Dice.Set(System.Convert.ToInt32(Temp.GUID), iEquipProb);
			}//if
			
			Itor.Next();
		}//while
		
		if(iEmptyProb > 0)
			Dice.Set(0, iEmptyProb); // 加入失敗項

		return Dice.Roll();
	}
	// 新增隊伍成員
	public static void MemberPartyAdd(int iLooks, int iEquip)
	{
		Member Temp = new Member();
		
		Temp.iLooks = iLooks;
		Temp.iEquip = iEquip;
        Temp.fReactTime = Random.Range(0.00f,0.25f);

		DataPlayer.pthis.MemberParty.Add(Temp);
	}
	// 新增隊伍成員
	public static void MemberPartyAdd(int iEquip)
	{
		MemberPartyAdd(RandomMemberLooks(), iEquip);
	}
	// 新增隊伍成員
	public static void MemberPartyAdd()
	{
		MemberPartyAdd(0);
	}
	// 刪除隊伍成員
	public static void MemberPartyDel(int iPos)
	{
		DataPlayer.pthis.MemberParty.RemoveAt(iPos);
	}
	// 新增角色庫成員
	public static void MemberDepotAdd(int iLooks, int iEquip)
	{
		if(DataPlayer.pthis.MemberDepot.Count >= GameDefine.iMaxMemberDepot)
			return;

		Member Temp = new Member();
		
		Temp.iLooks = iLooks;
		Temp.iEquip = iEquip;
        Temp.fReactTime = Random.Range(0.00f, 0.20f);
		
		DataPlayer.pthis.MemberDepot.Add(Temp);
	}
	// 新增角色庫成員
	public static void MemberDepotAdd(int iLooks)
	{
		MemberDepotAdd(iLooks, RandomEquipParty(false, GameDefine.iEquipExtraDepot));
	}
	// 新增角色庫成員
	public static void MemberDepotAdd()
	{
		MemberDepotAdd(RandomMemberLooks());
	}
	// 刪除角色庫成員
	public static void MemberDepotDel(int iPos)
	{
		DataPlayer.pthis.MemberParty.RemoveAt(iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset(int iPos)
	{
		if(DataPlayer.pthis.MemberParty.Count > iPos)
			DataPlayer.pthis.MemberParty[iPos].fCriticalStrike = FeatureF(ENUM_ModeFeature.CriticalStrike, iPos);
	}
	// 重置成員致命值
	public static void CriticalStrikeReset()
	{
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			CriticalStrikeReset(iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset(int iPos)
	{
		if(DataPlayer.pthis.MemberParty.Count > iPos)
			DataPlayer.pthis.MemberParty[iPos].iAddDamage = FeatureI(ENUM_ModeFeature.AddDamage, iPos);
	}
	// 重置成員增傷值
	public static void AddDamageReset()
	{
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			AddDamageReset(iPos);
	}
	// 執行獲得特性
	public static int GainFeature(int iPos)
	{
		if(DataPlayer.pthis.MemberParty.Count <= iPos)
			return 0;
		
		if(DataPlayer.pthis.MemberParty[iPos].Feature.Count >= GameDefine.iMaxFeature)
			return 0;
		
		// 建立特性群組列表
		HashSet<int> Group = new HashSet<int>();
		
		foreach(int ItorFeature in DataPlayer.pthis.MemberParty[iPos].Feature)
		{
			DBFFeature Data = GameDBF.pthis.GetFeature(ItorFeature) as DBFFeature;
			
			if(Data != null)
				Group.Add(Data.Group);
		}//for
		
		// 建立獲得特性骰子
		int iEmptyProb = 100;
		CDice<int> Dice = new CDice<int>();
		DBFItor Itor = GameDBF.pthis.GetFeature();
		
		while(Itor.IsEnd() == false)
		{
			DBFFeature Data = Itor.Data() as DBFFeature;
			
			if(Data != null && Group.Contains(Data.Group) == false)
			{
				iEmptyProb -= Data.Gain;
				Dice.Set(System.Convert.ToInt32(Data.GUID), Data.Gain);
			}//if
			
			Itor.Next();
		}//while
		
		if(iEmptyProb > 0)
			Dice.Set(0, iEmptyProb); // 加入失敗項
		
		int iFeature = Dice.Roll();
		
		if(iFeature > 0)
			DataPlayer.pthis.MemberParty[iPos].Feature.Add(iFeature);
		
		return iFeature;
	}
	// 隊伍成員執行獲得裝備
	public static int GainEquip(int iPos)
	{
		if(DataPlayer.pthis.MemberParty.Count <= iPos)
			return 0;
		
		if(DataPlayer.pthis.MemberParty[iPos].iEquip > 0)
			return 0;
		
		// 檢查隊伍裡是否沒有光源裝備, 設定裝備額外機率值
		bool bLight = false;
		int iEquipExtra = 0;
		
		foreach(Member ItorMember in DataPlayer.pthis.MemberParty)
		{
			DBFEquip Data = GameDBF.pthis.GetEquip(ItorMember.iEquip) as DBFEquip;

			if(Data == null)
			{
				iEquipExtra += GameDefine.iEquipExtraParty;
				continue;
			}//if
			
			if(Data.Mode == (int)ENUM_ModeEquip.Light)
				bLight = true;
		}//for

		return DataPlayer.pthis.MemberParty[iPos].iEquip = RandomEquipParty(bLight == false, iEquipExtra);
	}
	// 取得子彈傷害值
	public static Tuple<int, bool> BulletDamage(int iPos, bool bEnable)
	{
		int iDamage = 0;
		bool bCriticalStrike = false;

		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			Member DataMember = DataPlayer.pthis.MemberParty[iPos];
			DBFEquip DataEquip = GameDBF.pthis.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;

			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
			{
				if(bEnable && Random.Range(0.0f, GameDefine.fCriticalStrikProb) <= (DataMember.fCriticalStrike + DataEquip.CriticalStrike))
				{
					float fCriticalStrik = 0.0f;
					
					if(System.Convert.ToInt32(DataEquip.GUID) == (int)ENUM_Weapon.Eagle && GetWeaponLevel(ENUM_Weapon.Eagle) > 0)
						fCriticalStrik = UpgradeWeaponEagle();
					else
						fCriticalStrik = GameDefine.fCriticalStrik;
					
					iDamage = (int)((DataMember.iAddDamage + DataEquip.Damage) * fCriticalStrik);
					bCriticalStrike = true;
				}
				else
				{
					iDamage = (DataMember.iAddDamage + DataEquip.Damage);
					bCriticalStrike = false;
				}//if

				iDamage += DataMember.iLiveStage * GameDefine.iDamageUpgrade + DataPlayer.pthis.iDamageLv;
			}//if
		}//if

		return new Tuple<int, bool>(iDamage, bCriticalStrike);
	}
	// 取得裝備攻擊間隔時間
	public static float EquipFireRate(int iPos)
	{
		float fResult = 0;

		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			Member DataMember = DataPlayer.pthis.MemberParty[iPos];
			DBFEquip DataEquip = GameDBF.pthis.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;
			
			if(DataEquip != null && DataEquip.Mode == (int)ENUM_ModeEquip.Damage)
			{
				if(System.Convert.ToInt32(DataEquip.GUID) == (int)ENUM_Weapon.Knife && GetWeaponLevel(ENUM_Weapon.Knife) > 0)
					fResult = UpgradeWeaponKnife();
				else
					fResult = DataEquip.FireRate;
			}//if
		}//if
		
		return fResult;
	}
	// 取得可產生怪物列表
	public static List<int> MonsterList()
	{
		List<int> Result = new List<int>();
		DBFItor Itor = GameDBF.pthis.GetMonster();

		while(Itor.IsEnd() == false)
		{
			DBFMonster Data = Itor.Data() as DBFMonster;

			if(Data != null && Data.StageID <= DataPlayer.pthis.iStage)
				Result.Add(System.Convert.ToInt32(Data.GUID));

			Itor.Next();
		}//while

		return Result;
	}
	// 取得成員威脅值
	public static int MemberThreat(int iPos)
	{
		int iResult = 0;

		if(DataPlayer.pthis.MemberParty.Count > iPos)
		{
			Member DataMember = DataPlayer.pthis.MemberParty[iPos];

			iResult += DataMember.iLiveStage + 1;

			DBFEquip DataEquip = GameDBF.pthis.GetEquip(new Argu(DataMember.iEquip)) as DBFEquip;

			if(DataEquip != null)
				iResult += DataEquip.Threat;

			foreach(int Itor in DataMember.Feature)
			{
				DBFFeature DataFeature = GameDBF.pthis.GetFeature(new Argu(Itor)) as DBFFeature;

				if(DataFeature != null)
					iResult += DataFeature.Threat;
			}//for
		}//if

		return Mathf.Max(0, iResult);
	}
	// 取得傷害等級價格
	public static int DmgLvMoney()
	{
		return (int)(GameDefine.iPriceDmgLv + GameDefine.iPriceDmgLv * GameDefine.fUpgradeDmgLv * DataPlayer.pthis.iDamageLv);
	}
	// 取得廣告影片贈送金額
	public static int AdsMoney()
	{
		return GameDefine.iAdsMoneyBase + GameDefine.iAdsMoneyAdd * DataPlayer.pthis.iAdsWatch;
	}
	// 取得魔王血量加成
	public static int BossHP(int iHP)
	{
		return iHP + (int)(iHP * GameDefine.fUpgradeBossHP * DataPlayer.pthis.iStage);
	}
	// 重設成就內容
	public static void AchievementReset(ENUM_Achievement emAchievement)
	{
		DataAchievement.pthis.Data[(int)emAchievement] = 0;
	}
	// 增加成就內容, 傳回完成成就等級列表
	public static List<int> AchievementAdd(ENUM_Achievement emAchievement, int iValue)
	{
		List<int> Result = new List<int>();
		DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(emAchievement);

		if(DBFTemp == null)
			return Result;

		if(iValue <= 0)
			return Result;

		int iOldValue = DataAchievement.pthis.GetValue(emAchievement);
		int iNewValue = iOldValue + iValue;
		int iMaxValue = DBFTemp.GetValue(DBFTemp.MaxLevel);

		if(iOldValue >= iMaxValue)
			return Result;

		iNewValue = iNewValue > iMaxValue ? iMaxValue : iNewValue;
		DataAchievement.pthis.Data[(int)emAchievement] = iNewValue;

		for(int iLevel = 1; iLevel <= DBFTemp.MaxLevel; ++iLevel)
		{
			int iLevelValue = DBFTemp.GetValue(iLevel);

			if(iLevelValue >= iOldValue && iLevelValue <= iNewValue)
				Result.Add(iLevel);
		}//for

		return Result;
	}	
	// 取得提供給介面顯示用的成就列表(包含成就列舉, 等級)
	public static List<Tuple<ENUM_Achievement, int>> AchievementShow()
	{
		List<Tuple<ENUM_Achievement, int>> ResultProgress = new List<Tuple<ENUM_Achievement, int>>();
		List<Tuple<ENUM_Achievement, int>> ResultFinished = new List<Tuple<ENUM_Achievement, int>>();
		
		DBFItor Itor = GameDBF.pthis.GetAchievement();
		
		while(Itor.IsEnd() == false)
		{
			DBFAchievement DBFTemp = (DBFAchievement)Itor.Data();
			ENUM_Achievement emAchievement = (ENUM_Achievement)System.Convert.ToInt32(DBFTemp.GUID);
			int iValue = DataAchievement.pthis.GetValue(emAchievement);
			
			for(int iLevel = 1; iLevel <= DBFTemp.MaxLevel; ++iLevel)
			{
				Tuple<ENUM_Achievement, int> ResultTemp = new Tuple<ENUM_Achievement, int>(emAchievement, iLevel);

				if(iValue < DBFTemp.GetValue(iLevel))
					ResultProgress.Add(ResultTemp);
				else
					ResultFinished.Add(ResultTemp);
			}//for
			
			Itor.Next();
		}//while
		
		List<Tuple<ENUM_Achievement, int>> Result = new List<Tuple<ENUM_Achievement, int>>();
		
		Result.AddRange(ResultProgress);
		Result.AddRange(ResultFinished);
		
		return Result;
	}
	// 拾取收集物品, 傳回是否完成
	public static bool CollectionAdd(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		if(DataCollection.pthis.Add(Weapon, iLevel, iIndex) == false)
			return false;

		bool bResult = true;

		for(int iPos = 1; iPos <= GameDefine.iMaxCollectionCount; ++iPos)
			bResult &= DataCollection.pthis.IsExist(Weapon, iLevel, iPos);

		return bResult;
	}
	// 給予獎勵
	public static void GetReward(int iReward)
	{
		DBFReward DBFTemp = (DBFReward)GameDBF.pthis.GetReward(iReward);

		if(DBFTemp == null)
			return;

		ENUM_Reward emReward = (ENUM_Reward)DBFTemp.Reward;

		if(emReward == ENUM_Reward.Looks)
		{
			DataReward.pthis.MemberLooks.Add(DBFTemp.Value);
			DataReward.pthis.MemberInits.Add(DBFTemp.Value);
		}//if
		
		if(emReward == ENUM_Reward.Currency)
			DataReward.pthis.iInitCurrency += DBFTemp.Value;
		
		if(emReward == ENUM_Reward.Battery)
			DataReward.pthis.iInitBattery += DBFTemp.Value;
		
		if(emReward == ENUM_Reward.LightAmmo)
			DataReward.pthis.iInitLightAmmo += DBFTemp.Value;
		
		if(emReward == ENUM_Reward.HeavyAmmo)
			DataReward.pthis.iInitHeavyAmmo += DBFTemp.Value;
		
		if(emReward == ENUM_Reward.Bomb)
			DataReward.pthis.iInitBomb += DBFTemp.Value;
	}
	// 增加武器等級
	public static void AddWeaponLevel(ENUM_Weapon emWeapon)
	{
		int iWeapon = (int)emWeapon;

		if(DataReward.pthis.WeaponLevel.ContainsKey(iWeapon))
			DataReward.pthis.WeaponLevel[iWeapon]++;
		else
			DataReward.pthis.WeaponLevel[iWeapon] = 1;
	}
	// 取得武器等級
	public static int GetWeaponLevel(ENUM_Weapon emWeapon)
	{
		int iWeapon = (int)emWeapon;

		return DataReward.pthis.WeaponLevel.ContainsKey(iWeapon) ? DataReward.pthis.WeaponLevel[iWeapon] : 0;
	}
	// 取得升級手電筒:灼燒傷害與下次時間
	public static Tuple<int, float> UpgradeWeaponLight(int iLevel)
	{
		return new Tuple<int, float>(iLevel * 6, Time.realtimeSinceStartup + 2.0f);
	}
	// 取得升級手電筒:灼燒傷害與下次時間
	public static Tuple<int, float> UpgradeWeaponLight()
	{
		return UpgradeWeaponLight(GetWeaponLevel(ENUM_Weapon.Light));
	}
	// 取得升級小刀:加速的攻速值
	public static float UpgradeWeaponKnife(int iLevel)
	{
		DBFEquip DBFTemp = GameDBF.pthis.GetEquip(new Argu((int)ENUM_Weapon.Knife)) as DBFEquip;

		return DBFTemp != null ? (float)System.Math.Round((double)Mathf.Max(1.0f - iLevel * 0.15f, 0.0f) * DBFTemp.FireRate, 1) : 999.0f;
	}
	// 取得升級小刀:加速的攻速值
	public static float UpgradeWeaponKnife()
	{
		return UpgradeWeaponKnife(GetWeaponLevel(ENUM_Weapon.Knife));
	}
	// 取得升級手槍:冰凍的緩速值
	public static float UpgradeWeaponPistol(int iLevel)
	{
		return (float)System.Math.Round((double)Mathf.Max(1.0f - (0.14f + iLevel * 0.06f), 0.0f), 2);
	}
	// 取得升級手槍:冰凍的緩速值
	public static float UpgradeWeaponPistol()
	{
		return UpgradeWeaponPistol(GetWeaponLevel(ENUM_Weapon.Pistol));
	}
	// 取得升級左輪手槍:連鎖的連鎖次數
	public static int UpgradeWeaponRevolver(int iLevel)
	{
		return iLevel;
	}
	// 取得升級左輪手槍:連鎖的連鎖次數
	public static int UpgradeWeaponRevolver()
	{
		return UpgradeWeaponRevolver(GetWeaponLevel(ENUM_Weapon.Revolver));
	}
	// 取得升級左輪手槍:連鎖的傷害值
	public static int UpgradeWeaponRevolver(int iDamage, int iCount)
	{
		return Mathf.Max(iDamage - iCount * 2, 0);
	}
	// 取得升級沙漠之鷹:爆頭的致命傷害倍數
	public static float UpgradeWeaponEagle(int iLevel)
	{
		return Mathf.Max(280.0f + (iLevel - 1) * 10.0f, 0.0f);
	}
	// 取得升級沙漠之鷹:爆頭的致命傷害倍數
	public static float UpgradeWeaponEagle()
	{
		return UpgradeWeaponEagle(GetWeaponLevel(ENUM_Weapon.Eagle));
	}
	// 取得升級衝鋒槍:榴彈的爆炸傷害值
	public static int UpgradeWeaponSUB(int iLevel)
	{
		return iLevel * 6;
	}
	// 取得升級衝鋒槍:榴彈的爆炸傷害值
	public static int UpgradeWeaponSUB()
	{
		return UpgradeWeaponSUB(GetWeaponLevel(ENUM_Weapon.SUB));
	}
	// 取得升級突擊步槍:穿透的穿透次數
	public static int UpgradeWeaponRifle(int iLevel)
	{
		return iLevel;
	}
	// 取得升級突擊步槍:穿透的穿透次數
	public static int UpgradeWeaponRifle()
	{
		return UpgradeWeaponRifle(GetWeaponLevel(ENUM_Weapon.Rifle));
	}
	// 取得升級輕機槍:電漿的增傷值
	public static int UpgradeWeaponLMG(int iLevel)
	{
		return iLevel;
	}
	// 取得升級輕機槍:電漿的增傷值
	public static int UpgradeWeaponLMG()
	{
		return UpgradeWeaponLMG(GetWeaponLevel(ENUM_Weapon.LMG));
	}
    // 隨機過關的的水晶物品.
    public static void RandomCollect()
    {
        for(int i = 0; i < DataGame.pthis.iWeaponType.Length; ++i)
        {
			DataGame.pthis.iWeaponType[i] = (int)ENUM_Weapon.Null;
			DataGame.pthis.iWeaponIndex[i] = 0;

			if(DataReward.pthis.iCrystal <= 0 && DataPlayer.pthis.iStage <= GameDefine.iCrystalStage)
				continue;

			ENUM_Weapon emWeapon = ENUM_Weapon.Null;
			int iLevel = 0;
			int iRatio = 0;

			if(GetWeaponLevel(ENUM_Weapon.Pistol) <= 0)
			{
				emWeapon = ENUM_Weapon.Pistol;
				iLevel = 1;
				iRatio = 0;
			}
			else
			{
				emWeapon = (ENUM_Weapon)Random.Range((int)ENUM_Weapon.Null + 1, (int)ENUM_Weapon.Count);
				iLevel = System.Math.Min(GetWeaponLevel(emWeapon) + 1, GameDefine.iMaxCollectionLv);
				iRatio = Random.Range(0, 100);
			}//if

			if(iRatio <= GameDefine.iCollectionRatio)
			{
				List<int> Temp = new List<int>();

				for(int iPos = 1; iPos <= GameDefine.iMaxCollectionCount; ++iPos)
				{
					if(DataGame.pthis.iWeaponIndex.Any(id => iPos == id))
						continue;

					if(DataCollection.pthis.IsExist(emWeapon, iLevel, iPos))
						continue;

					Temp.Add(iPos);
				}//for

				if(Temp.Count > 0)
				{
					DataGame.pthis.iWeaponType[i] = (int)emWeapon;
					DataGame.pthis.iWeaponIndex[i] = Tool.RandomPick(Temp);
				}//if
			}//if
        }//for

        DataGame.pthis.Save();
    }
	// 取得是否要出現魔王關
	public static bool AppearBossStage()
	{
		return DataPlayer.pthis.iStage % GameDefine.iBossStage == 0;
	}
}
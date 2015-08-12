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
		PickupStat.pthis.Record(ENUM_Pickup.Crystal, iValue);
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
		PickupStat.pthis.Record(ENUM_Pickup.Currency, iValue);
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
	// 重置絕招次數
	public static void BombReset()
	{
		DataPlayer.pthis.iBomb = Value(0, GameDefine.iMaxBomb, Mathf.Max(DataPlayer.pthis.iBomb, FeatureI(ENUM_ModeFeature.AddLeastBomb)));
	}
	// 增加絕招次數
	public static void BombAdd(int iValue)
	{
		DataPlayer.pthis.iBomb = Value(0, GameDefine.iMaxBomb, DataPlayer.pthis.iBomb + iValue);
		PickupStat.pthis.Record(ENUM_Pickup.Bomb, iValue);
	}
	// 重置護盾次數
	public static void ShieldReset()
	{
		for(int iPos = 0; iPos < DataPlayer.pthis.MemberParty.Count; ++iPos)
			DataPlayer.pthis.MemberParty[iPos].iShield = Value(0, GameDefine.iMaxShield, FeatureI(ENUM_ModeFeature.AddShield, iPos));
	}
	// 重置資源
	public static void ResourceReset(ENUM_Resource emResource)
	{
		while(DataPlayer.pthis.Resource.Count <= (int)emResource)
			DataPlayer.pthis.Resource.Add(0);

		DataPlayer.pthis.Resource[(int)emResource] = 0;
	}
	// 增加資源
	public static void ResourceAdd(ENUM_Resource emResource, int iValue)
	{
		while(DataPlayer.pthis.Resource.Count <= (int)emResource)
			DataPlayer.pthis.Resource.Add(0);

		int iResult = DataPlayer.pthis.Resource[(int)emResource];

		switch(emResource)
		{
		case ENUM_Resource.Battery:
			iResult = Value(0, System.Int32.MaxValue, iResult + iValue);
			PickupStat.pthis.Record(ENUM_Pickup.Battery, iValue);
			break;

		case ENUM_Resource.LightAmmo:
			iResult = Value(0, GameDefine.iMaxLightAmmo, iResult + iValue);
			PickupStat.pthis.Record(ENUM_Pickup.LightAmmo, iValue);
			break;

		case ENUM_Resource.HeavyAmmo:
			iResult = Value(0, GameDefine.iMaxHeavyAmmo, iResult + iValue);
			PickupStat.pthis.Record(ENUM_Pickup.HeavyAmmo, iValue);
			break;

		default: break;
		}//switch

		DataPlayer.pthis.Resource[(int)emResource] = iResult;
	}
	// 檢查是否資源足夠
	public static bool ResourceChk(ENUM_Resource emResource, int iValue)
	{
		if(emResource == ENUM_Resource.Null)
			return true;

		int iIndex = (int)emResource;

		if(DataPlayer.pthis.Resource.Count <= iIndex)
			return false;

		return DataPlayer.pthis.Resource[iIndex] > iValue;
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
		if(DataPlayer.pthis.MemberDepot.Count >= GameDefine.iMaxMemberDepotCount)
			return;

		Member Temp = new Member();
		
		Temp.iLooks = iLooks;
		Temp.iEquip = iEquip;
		
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
		if(DataMap.pthis.DataRoad.Count <= GameDefine.iPickupBorder)
			return new MapCoor();

		MapCoor Road = DataMap.pthis.DataRoad[Random.Range(GameDefine.iPickupBorder, DataMap.pthis.DataRoad.Count - GameDefine.iPickupBorder)];

		for(int iCount = 0; iCount < GameDefine.iPickupSearch; ++iCount)
		{
			MapCoor Result = new MapCoor(Road.X + Tool.RandomPick(GameDefine.PickupRange), Road.Y + Tool.RandomPick(GameDefine.PickupRange));
			bool bCheck = true;

			foreach(MapCoor Itor in DataMap.pthis.DataRoad)
				bCheck &= (Result.X == Itor.X && Result.Y == Itor.Y) == false;

			foreach(Pickup Itor in DataGame.pthis.PickupList)
				bCheck &= (Result.X == Itor.Pos.X && Result.Y == Itor.Pos.Y) == false;

			if(bCheck)
				return Result;
		}//for

		return new MapCoor();
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

		return DataPlayer.pthis.MemberParty[iPos].iEquip = RandomEquipParty(bLight, iEquipExtra);
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
				fResult = DataEquip.FireRate;
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
		return (int)(iHP * GameDefine.fUpgradeBossHP * DataPlayer.pthis.iStage);
	}
	// 增加成就內容, 傳回完成成就等級列表
	public static List<int> AchievementAdd(ENUM_Achievement emAchievement, int iValue)
	{
		List<int> Result = new List<int>();
		DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(emAchievement);

		if(DBFTemp == null)
			return Result;

		int iAchievement = (int)emAchievement;
		int iOldValue = DataAchievement.pthis.Data.ContainsKey(iAchievement) ? DataAchievement.pthis.Data[iAchievement] : 0;
		int iNewValue = iOldValue + iValue;

		iNewValue = iNewValue > DBFTemp.Lv6Value ? DBFTemp.Lv6Value : iNewValue;
		DataAchievement.pthis.Data[iAchievement] = iNewValue;
		
		if(DBFTemp.Lv1Value >= iOldValue && DBFTemp.Lv1Value <= iNewValue)
			Result.Add(1);
		
		if(DBFTemp.Lv2Value >= iOldValue && DBFTemp.Lv2Value <= iNewValue)
			Result.Add(2);
		
		if(DBFTemp.Lv3Value >= iOldValue && DBFTemp.Lv3Value <= iNewValue)
			Result.Add(3);
		
		if(DBFTemp.Lv4Value >= iOldValue && DBFTemp.Lv4Value <= iNewValue)
			Result.Add(4);
		
		if(DBFTemp.Lv5Value >= iOldValue && DBFTemp.Lv5Value <= iNewValue)
			Result.Add(5);
		
		if(DBFTemp.Lv6Value >= iOldValue && DBFTemp.Lv6Value <= iNewValue)
			Result.Add(6);

		DataAchievement.pthis.Data[iAchievement] = iNewValue;

		return Result;
	}
	// 取得進行中成就資訊
	public static AchievementInfo AchievementRunning(ENUM_Achievement emAchievement)
	{
		DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(emAchievement);
		
		if(DBFTemp == null)
			return new AchievementInfo();
				
		int iAchievement = (int)emAchievement;
		int iValue = DataAchievement.pthis.Data.ContainsKey(iAchievement) ? DataAchievement.pthis.Data[iAchievement] : 0;

		if(DBFTemp.Lv1Value > iValue)
			return new AchievementInfo(1, iValue);
		
		if(DBFTemp.Lv2Value > iValue)
			return new AchievementInfo(2, iValue);
		
		if(DBFTemp.Lv3Value > iValue)
			return new AchievementInfo(3, iValue);
		
		if(DBFTemp.Lv4Value > iValue)
			return new AchievementInfo(4, iValue);
		
		if(DBFTemp.Lv5Value > iValue)
			return new AchievementInfo(5, iValue);

		return new AchievementInfo(6, iValue);
	}
	// 拾取收集物品, 傳回完成收集編號, 等級列表
	public static List<Tuple<int, int>> CollectionAdd(int iItems)
	{
		List<Tuple<int, int>> Result = new List<Tuple<int, int>>();

		if(DataCollection.pthis.IsCollection(iItems) == false)
			return Result;

		if(DataCollection.pthis.Data.Contains(iItems))
			return Result;

		DataCollection.pthis.Data.Add(iItems);

		DBFItor Itor = GameDBF.pthis.GetCollection();

		while(Itor.IsEnd() == false)
		{
			DBFCollection DBFTemp = (DBFCollection)Itor.Data();

			for(int iLevel = GameDefine.iMinCollectionLv; iLevel <= GameDefine.iMaxCollectionLv; ++iLevel)
			{
				List<int> Items = DBFTemp.Items(iLevel);

				if(Items.Contains(iItems) == false)
					continue;

				if(DataCollection.pthis.IsComplete(Items) == false)
					continue;

				Result.Add(new Tuple<int, int>(System.Convert.ToInt32(DBFTemp.GUID), iLevel));
			}//for

			Itor.Next();
		}//while

		return Result;
	}
	// 取得進行中收集資訊
	public static CollectionInfo CollectionRunning(int iGUID)
	{
		DBFCollection DBFTemp = (DBFCollection)GameDBF.pthis.GetCollection(iGUID);
		
		if(DBFTemp == null)
			return new CollectionInfo();

		for(int iLevel = GameDefine.iMinCollectionLv; iLevel <= GameDefine.iMaxCollectionLv; ++iLevel)
		{
			List<int> Items = DBFTemp.Items(iLevel);

			if(DataCollection.pthis.IsComplete(Items))
				continue;

			CollectionInfo Result = new CollectionInfo();

			Result.iLevel = iLevel;
			Result.Items = DataCollection.pthis.GetComplete(Items);
			
			return Result;
		}//for

		return new CollectionInfo();
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
		
		if(emReward == ENUM_Reward.Upgrade)
		{
			int iTemp = DataReward.pthis.WeaponLevel.ContainsKey(DBFTemp.Value) ? DataReward.pthis.WeaponLevel[DBFTemp.Value] : 0;
			
			DataReward.pthis.WeaponLevel[DBFTemp.Value] = iTemp + 1;
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
	// 取得武器等級
	public static int GetWeaponLevel(ENUM_Weapon emWeapon)
	{
		int iWeapon = (int)emWeapon;

		return DataReward.pthis.WeaponLevel.ContainsKey(iWeapon) ? DataReward.pthis.WeaponLevel[iWeapon] : 0;
	}
	// 取得升級手槍:冰凍的緩速值
	public static float UpgradeWeaponPistol()
	{
		return Mathf.Max(100.0f - (20.0f + GetWeaponLevel(ENUM_Weapon.Pistol) - 1) * 4.0f, 0.0f);
	}
	// 取得升級左輪手槍:連鎖的連鎖次數
	public static int UpgradeWeaponRevolver()
	{
		return GetWeaponLevel(ENUM_Weapon.Revolver);
	}
	// 取得升級沙漠之鷹:爆頭的致命傷害倍數
	public static float UpgradeWeaponEagle()
	{
		return Mathf.Max(280.0f + (GetWeaponLevel(ENUM_Weapon.Eagle) - 1) * 10.0f, 0.0f);
	}
	// 取得升級衝鋒槍:榴彈的爆炸傷害值
	public static int UpgradeWeaponSUB()
	{
		return GetWeaponLevel(ENUM_Weapon.SUB) * 6;
	}
	// 取得升級突擊步槍:穿透的穿透次數
	public static int UpgradeWeaponRifle()
	{
		return GetWeaponLevel(ENUM_Weapon.Rifle);
	}
	// 取得升級輕機槍:電漿的增傷值
	public static int UpgradeWeaponLMG()
	{
		return GetWeaponLevel(ENUM_Weapon.LMG);
	}
	// 取得是否要出現模王關
	public static bool AppearBossStage()
	{
		return DataPlayer.pthis.iStage % GameDefine.iBossStage == 0;
	}
	// 取得是否要出現水晶
	public static bool AppearCrystal()
	{
		if(AppearBossStage())
			return true;

		if(DataPlayer.pthis.iStage >= GameDefine.iCrystalStage && Random.Range(0, 100) <= GameDefine.iCrystalRatio)
			return true;

		return false;
	}
}
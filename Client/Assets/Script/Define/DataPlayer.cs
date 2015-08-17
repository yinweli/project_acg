using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataPlayer : MonoBehaviour 
{
    static public DataPlayer pthis = null;

	/* Save */
	public int iStage = 0; // 關卡編號
	public int iStyle = 0; // 關卡風格編號
	public int iCurrency = 0; // 通貨
	public int iBattery = 0; // 電池
	public int iLightAmmo = 0; // 輕型彈藥
	public int iHeavyAmmo = 0; // 重型彈藥
	public int iBomb = 0; // 絕招次數
	public int iStamina = 0; // 耐力
	public int iDamageLv = 0; // 傷害等級
	public int iPlayTime = 0; // 遊戲時間
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayerLost = 0; // 死人數量
	public int iAdsWatch = 0; // 觀看廣告次數
	public List<Member> MemberParty = new List<Member>(); // 成員列表
	public List<Member> MemberDepot = new List<Member>(); // 角色庫列表

	/* Not Save */
	public int iStaminaLimit = 0; // 耐力上限
	public int iStaminaRecovery = 0; // 耐力回復

    void Awake()
    {
        pthis = this;
    }
	private SaveMember[] MemberSave(List<Member> Data)
	{
		List<SaveMember> Result = new List<SaveMember>();

		foreach(Member Itor in Data)
		{
			SaveMember Temp = new SaveMember();

			Temp.iLooks = Itor.iLooks;
			Temp.iEquip = Itor.iEquip;
			Temp.iLiveStage = Itor.iLiveStage;
			Temp.iShield = Itor.iShield;
			Temp.Feature = Itor.Feature.ToArray();
			Temp.Behavior = Itor.Behavior.ToArray();

			Result.Add(Temp);
		}//for

		return Result.ToArray();
	}
	private List<Member> MemberLoad(SaveMember[] Data)
	{
		List<Member> Result = new List<Member>();

		foreach(SaveMember Itor in Data)
		{
			Member Temp = new Member();
			
			Temp.iLooks = Itor.iLooks;
			Temp.iEquip = Itor.iEquip;
			Temp.iLiveStage = Itor.iLiveStage;
			Temp.iShield = Itor.iShield;
			Temp.Feature = new List<int>(Itor.Feature);
			Temp.Behavior = new List<int>(Itor.Behavior);
			
			Result.Add(Temp);
		}//for

		return Result;
	}
	// 存檔.
	public void Save()
	{
		SavePlayer Temp = new SavePlayer();
		
		Temp.iStage = iStage;
		Temp.iStyle = iStyle;
		Temp.iCurrency = iCurrency;
		Temp.iBattery = iBattery;
		Temp.iLightAmmo = iLightAmmo;
		Temp.iHeavyAmmo = iHeavyAmmo;
		Temp.iBomb = iBomb;
		Temp.iStamina = iStamina;
		Temp.iDamageLv = iDamageLv;
		Temp.iPlayTime = iPlayTime;
		Temp.iEnemyKill = iEnemyKill;
		Temp.iPlayerLost = iPlayerLost;
		Temp.iAdsWatch = iAdsWatch;
		Temp.Party = MemberSave(MemberParty);
		Temp.Depot = MemberSave(MemberDepot);
		
		PlayerPrefs.SetString(GameDefine.szSavePlayer, Json.ToString(Temp));
	}
    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSavePlayer) == false)
			return false;
		
		SavePlayer Temp = Json.ToObject<SavePlayer>(PlayerPrefs.GetString(GameDefine.szSavePlayer));
		
		if(Temp == null)
			return false;
		
		iStage = Temp.iStage;
		iStyle = Temp.iStyle;
		iCurrency = Temp.iCurrency;
		iBattery = Temp.iBattery;
		iLightAmmo = Temp.iLightAmmo;
		iHeavyAmmo = Temp.iHeavyAmmo;
		iBomb = Temp.iBomb;
		iStamina = Temp.iStamina;
		iDamageLv = Temp.iDamageLv;
		iPlayTime = Temp.iPlayTime;
		iEnemyKill = Temp.iEnemyKill;
		iPlayerLost = Temp.iPlayerLost;
		iAdsWatch = Temp.iAdsWatch;
		MemberParty = MemberLoad(Temp.Party);
		MemberDepot = MemberLoad(Temp.Depot);
		
		return true;
	}
	// 清除資料
	public void Clear()
	{
		iStage = 0;
		iStyle = 0;
		iCurrency = 0;
		iBattery = 0;
		iLightAmmo = 0;
		iHeavyAmmo = 0;
		iBomb = 0;
		iStamina = 0;
		iDamageLv = 0;
		iPlayTime = 0;
		iEnemyKill = 0;
		iPlayerLost = 0;
		iAdsWatch = 0;
		MemberParty.Clear();
		MemberDepot.Clear();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSavePlayer);
	}
}
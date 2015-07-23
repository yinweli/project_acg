using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    static public PlayerData pthis = null;

	/* Save */
	public int iStage = 0; // 關卡編號
	public int iStyle = 0; // 關卡風格編號
	public int iCurrency = 0; // 通貨
	public int iStamina = 0; // 耐力
	public int iBomb = 0; // 絕招次數
	public int iDamageLv = 0; // 傷害等級
	public int iPlayTime = 0; // 遊戲時間
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayerLost = 0; // 死人數量
	public int iAdsWatch = 0; // 觀看廣告次數
	public List<int> Resource = new List<int>(); // 資源列表
	public List<Member> Members = new List<Member>(); // 成員列表

	/* Not Save */
	public int iStaminaLimit = 0; // 耐力上限
	public int iStaminaRecovery = 0; // 耐力回復

    void Awake()
    {
        pthis = this;
    }
	// 存檔.
	public void Save()
	{
		SavePlayer Data = new SavePlayer();
		
		Data.iStage = iStage;
		Data.iStyle = iStyle;
		Data.iCurrency = iCurrency;
		Data.iStamina = iStamina;
		Data.iBomb = iBomb;
		Data.iDamageLv = iDamageLv;
		Data.iPlayTime = iPlayTime;
		Data.iEnemyKill = iEnemyKill;
		Data.iPlayerLost = iPlayerLost;
		Data.iAdsWatch = iAdsWatch;
		Data.Resource = Resource.ToArray();
		
		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in Members)
		{
			SaveMember Temp = new SaveMember();
			
			Temp.iLooks = Itor.iLooks;
			Temp.iEquip = Itor.iEquip;
			Temp.iLiveStage = Itor.iLiveStage;
			Temp.iShield = Itor.iShield;
			Temp.Feature = Itor.Feature.ToArray();
			Temp.Behavior = Itor.Behavior.ToArray();
			
			MemberList.Add(Temp);
		}//for
		
		Data.Data = MemberList.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSavePlayer, Json.ToString(Data));
	}
    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSavePlayer) == false)
			return false;
		
		SavePlayer Data = Json.ToObject<SavePlayer>(PlayerPrefs.GetString(GameDefine.szSavePlayer));
		
		if(Data == null)
			return false;
		
		iStage = Data.iStage;
		iStyle = Data.iStyle;
		iCurrency = Data.iCurrency;
		iStamina = Data.iStamina;
		iBomb = Data.iBomb;
		iDamageLv = Data.iDamageLv;
		iPlayTime = Data.iPlayTime;
		iEnemyKill = Data.iEnemyKill;
		iPlayerLost = Data.iPlayerLost;
		iAdsWatch = Data.iAdsWatch;
		Resource = new List<int>(Data.Resource);
		Members = new List<Member>();
		
		foreach(SaveMember Itor in Data.Data)
		{
			Member MemberTemp = new Member();
			
			MemberTemp.iLooks = Itor.iLooks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.iLiveStage = Itor.iLiveStage;
			MemberTemp.iShield = Itor.iShield;
			MemberTemp.Feature = new List<int>(Itor.Feature);
			MemberTemp.Behavior = new List<int>(Itor.Behavior);
			
			Members.Add(MemberTemp);
		}//for
		
		return true;
	}
	// 清除資料
	public void ClearData()
	{
		iStage = 0;
		iStyle = 0;
		iCurrency = 0;
		iStamina = 0;
		iBomb = 0;
		iDamageLv = 0;
		iPlayTime = 0;
		iEnemyKill = 0;
		iPlayerLost = 0;
		iAdsWatch = 0;
		Resource.Clear();
		Members.Clear();
	}
	// 清除存檔
	public void Clear()
	{
		ClearData();
		PlayerPrefs.DeleteKey(GameDefine.szSavePlayer);
	}
}
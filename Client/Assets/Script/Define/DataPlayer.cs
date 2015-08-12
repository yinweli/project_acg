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
	public int iStamina = 0; // 耐力
	public int iBomb = 0; // 絕招次數
	public int iDamageLv = 0; // 傷害等級
	public int iPlayTime = 0; // 遊戲時間
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayerLost = 0; // 死人數量
	public int iAdsWatch = 0; // 觀看廣告次數
	public List<int> Resource = new List<int>(); // 資源列表
	public List<Member> MemberParty = new List<Member>(); // 成員列表
	public List<Member> MemberDepot = new List<Member>(); // 角色庫列表

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
		SavePlayer Temp = new SavePlayer();
		
		Temp.iStage = iStage;
		Temp.iStyle = iStyle;
		Temp.iCurrency = iCurrency;
		Temp.iStamina = iStamina;
		Temp.iBomb = iBomb;
		Temp.iDamageLv = iDamageLv;
		Temp.iPlayTime = iPlayTime;
		Temp.iEnemyKill = iEnemyKill;
		Temp.iPlayerLost = iPlayerLost;
		Temp.iAdsWatch = iAdsWatch;
		Temp.Resource = Resource.ToArray();
		
		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in MemberParty)
		{
			SaveMember MemberTemp = new SaveMember();
			
			MemberTemp.iLooks = Itor.iLooks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.iLiveStage = Itor.iLiveStage;
			MemberTemp.iShield = Itor.iShield;
			MemberTemp.Feature = Itor.Feature.ToArray();
			MemberTemp.Behavior = Itor.Behavior.ToArray();
			
			MemberList.Add(MemberTemp);
		}//for
		
		Temp.Data = MemberList.ToArray();
		
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
		iStamina = Temp.iStamina;
		iBomb = Temp.iBomb;
		iDamageLv = Temp.iDamageLv;
		iPlayTime = Temp.iPlayTime;
		iEnemyKill = Temp.iEnemyKill;
		iPlayerLost = Temp.iPlayerLost;
		iAdsWatch = Temp.iAdsWatch;
		Resource = new List<int>(Temp.Resource);
		MemberParty = new List<Member>();
		
		foreach(SaveMember Itor in Temp.Data)
		{
			Member MemberTemp = new Member();
			
			MemberTemp.iLooks = Itor.iLooks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.iLiveStage = Itor.iLiveStage;
			MemberTemp.iShield = Itor.iShield;
			MemberTemp.Feature = new List<int>(Itor.Feature);
			MemberTemp.Behavior = new List<int>(Itor.Behavior);
			
			MemberParty.Add(MemberTemp);
		}//for
		
		return true;
	}
	// 清除資料
	public void Clear()
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
		MemberParty.Clear();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSavePlayer);
	}
}
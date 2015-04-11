using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    static public PlayerData pthis = null;

	/* Save */
	public int iStage = 0; // 關卡編號
	public int iCurrency = 0; // 通貨
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayTime = 0; // 遊戲時間
	public List<int> Resource = new List<int>(); // 資源列表
	public List<Member> Members = new List<Member>(); // 成員列表

	/* Not Save */
	public int iStamina = 0; // 耐力
	public int iStaminaLimit = 0; // 耐力上限
	public int iStaminaRecovery = 0; // 耐力回復

    void Awake()
    {
        pthis = this;
    }

    // 讀檔.
	private bool GameLoadPlayer()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSavePlayer) == false)
			return false;
		
		SavePlayer Data = Json.ToObject<SavePlayer>(PlayerPrefs.GetString(GameDefine.szSavePlayer));
		
		if(Data == null)
			return false;
		
		iStage = Data.iStage;
		iCurrency = Data.iCurrency;
		iEnemyKill = Data.iEnemyKill;
		iPlayTime = Data.iPlayTime;
		Resource = new List<int>(Data.Resource);
		Members = new List<Member>();
		
		foreach(SaveMember Itor in Data.Data)
		{
			Member MemberTemp = new Member();
			
			MemberTemp.Looks = Itor.Looks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.Feature = new List<int>(Itor.Feature);
			MemberTemp.Behavior = new List<int>(Itor.Behavior);
			
			Members.Add(MemberTemp);
		}//for
		
		return true;
	}

    // 存檔.
	private void GameSavePlayer()
	{
		SavePlayer Data = new SavePlayer();
		
		Data.iStage = iStage;
		Data.iCurrency = iCurrency;
		Data.iEnemyKill = iEnemyKill;
		Data.iPlayTime = iPlayTime;
		Data.Resource = Resource.ToArray();
		
		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in Members)
		{
			SaveMember Temp = new SaveMember();
			
			Temp.Looks = Itor.Looks;
			Temp.iEquip = Itor.iEquip;
			Temp.Feature = Itor.Feature.ToArray();
			Temp.Behavior = Itor.Behavior.ToArray();
			
			MemberList.Add(Temp);
		}//for
		
		Data.Data = MemberList.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSavePlayer, Json.ToString(Data));
	}
}
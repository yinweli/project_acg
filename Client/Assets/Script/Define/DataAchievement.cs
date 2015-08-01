using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataAchievement : MonoBehaviour
{
	static public DataAchievement pthis = null;
	
	/* Save */
	public int iSingle_Stage = 0; // 單輪通過關卡數量
	public int iSingle_SaveMember = 0; // 單輪救援成員人數
	public int iSingle_LostMember = 0; // 單輪失去成員人數
	public int iTotal_CurrencyPickup = 0; // 累積拾取金幣數量
	public int iTotal_MonsterKill = 0; // 累積擊敗怪物數量
	
	/* Not Save */
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SaveAchievement Temp = new SaveAchievement();
		
		Temp.iSingle_Stage = iSingle_Stage;
		Temp.iSingle_SaveMember = iSingle_SaveMember;
		Temp.iSingle_LostMember = iSingle_LostMember;
		Temp.iTotal_CurrencyPickup = iTotal_CurrencyPickup;
		Temp.iTotal_MonsterKill = iTotal_MonsterKill;
		
		PlayerPrefs.SetString(GameDefine.szSaveAchievement, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveAchievement) == false)
			return false;
		
		SaveAchievement Temp = Json.ToObject<SaveAchievement>(PlayerPrefs.GetString(GameDefine.szSaveAchievement));
		
		if(Temp == null)
			return false;
		
		iSingle_Stage = Temp.iSingle_Stage;
		iSingle_SaveMember = Temp.iSingle_SaveMember;
		iSingle_LostMember = Temp.iSingle_LostMember;
		iTotal_CurrencyPickup = Temp.iTotal_CurrencyPickup;
		iTotal_MonsterKill = Temp.iTotal_MonsterKill;
		
		return true;
	}
	// 清除單輪資料
	public void ClearSingle()
	{
		iSingle_Stage = 0;
		iSingle_SaveMember = 0;
		iSingle_LostMember = 0;
	}
	// 清除累積資料
	public void ClearTotal()
	{
		iTotal_CurrencyPickup = 0;
		iTotal_MonsterKill = 0;
	}
}
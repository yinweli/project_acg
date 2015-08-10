using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataAchievement : MonoBehaviour
{
	static public DataAchievement pthis = null;
	
	/* Save */
	public Dictionary<string, bool> Data = new Dictionary<string, bool>(); // 成就列表
	public int iSingle_Stage; // 單輪通過關卡數量
	public int iSingle_MemberSave; // 單輪救援隊員數量
	public int iSingle_MemberFire; // 單輪解雇隊員數量
	public int iTotal_Stage; // 累積通過關卡數量
	public int iTotal_Crystal; // 累積拾取水晶數量
	public int iTotal_Currency; // 累積拾取金幣數量
	public int iTotal_Battery; // 累積拾取電池數量
	public int iTotal_LightAmmo; // 累積拾取輕型彈藥數量
	public int iTotal_HeavyAmmo; // 累積拾取重型彈藥數量
	public int iTotal_Bomb; // 累積拾取絕招數量
	public int iTotal_Kill; // 累積擊敗怪物數量
	
	/* Not Save */
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		// 儲存成就
		{
			List<string> DataTemp = new List<string>();
			
			foreach(KeyValuePair<string, bool> Itor in Data)
				DataTemp.Add(Itor.Key + "_" + Itor.Value);
			
			SaveAchievement Temp = new SaveAchievement();
			
			Temp.Data = DataTemp.ToArray();
			
			PlayerPrefs.SetString(GameDefine.szSaveAchievement, Json.ToString(Temp));
		}

		// 儲存成就進度
		{
			SaveAchievementProgress Temp = new SaveAchievementProgress();

			Temp.iSingle_Stage = iSingle_Stage;
			Temp.iSingle_MemberSave = iSingle_MemberSave;
			Temp.iSingle_MemberFire = iSingle_MemberFire;
			Temp.iTotal_Stage = iTotal_Stage;
			Temp.iTotal_Crystal = iTotal_Crystal;
			Temp.iTotal_Currency = iTotal_Currency;
			Temp.iTotal_Battery = iTotal_Battery;
			Temp.iTotal_LightAmmo = iTotal_LightAmmo;
			Temp.iTotal_HeavyAmmo = iTotal_HeavyAmmo;
			Temp.iTotal_Bomb = iTotal_Bomb;
			Temp.iTotal_Kill = iTotal_Kill;
			
			PlayerPrefs.SetString(GameDefine.szSaveAchievementProgress, Json.ToString(Temp));
		}
	}
	// 讀檔.
	public bool Load()
	{
		// 讀取成就
		{
			if(PlayerPrefs.HasKey(GameDefine.szSaveAchievement) == false)
				return false;
			
			SaveAchievement Temp = Json.ToObject<SaveAchievement>(PlayerPrefs.GetString(GameDefine.szSaveAchievement));
			
			if(Temp == null)
				return false;
			
			foreach(string Itor in Temp.Data)
			{
				string[] szTemp = Itor.Split(new char[] {'_'});
				
				if(szTemp.Length >= 2)
					Data.Add(szTemp[0], System.Convert.ToBoolean(szTemp[1]));
			}//for
		}

		// 讀取成就進度
		{
			if(PlayerPrefs.HasKey(GameDefine.szSaveAchievementProgress) == false)
				return false;
			
			SaveAchievementProgress Temp = Json.ToObject<SaveAchievementProgress>(PlayerPrefs.GetString(GameDefine.szSaveAchievementProgress));
			
			if(Temp == null)
				return false;

			iSingle_Stage = Temp.iSingle_Stage;
			iSingle_MemberSave = Temp.iSingle_MemberSave;
			iSingle_MemberFire = Temp.iSingle_MemberFire;
			iTotal_Stage = Temp.iTotal_Stage;
			iTotal_Crystal = Temp.iTotal_Crystal;
			iTotal_Currency = Temp.iTotal_Currency;
			iTotal_Battery = Temp.iTotal_Battery;
			iTotal_LightAmmo = Temp.iTotal_LightAmmo;
			iTotal_HeavyAmmo = Temp.iTotal_HeavyAmmo;
			iTotal_Bomb = Temp.iTotal_Bomb;
			iTotal_Kill = Temp.iTotal_Kill;
		}
				
		return true;
	}
	// 設值
	public void Set(ENUM_Condition emCondition, int iValue)
	{
		switch(emCondition)
		{
		case ENUM_Condition.Single_Stage: iSingle_Stage = iValue; break;
		case ENUM_Condition.Single_MemberSave: iSingle_MemberSave = iValue; break;
		case ENUM_Condition.Single_MemberFire: iSingle_MemberFire = iValue; break;
		case ENUM_Condition.Total_Stage: iTotal_Stage = iValue; break;
		case ENUM_Condition.Total_Crystal: iTotal_Crystal = iValue; break;
		case ENUM_Condition.Total_Currency: iTotal_Currency = iValue; break;
		case ENUM_Condition.Total_Battery: iTotal_Battery = iValue; break;
		case ENUM_Condition.Total_LightAmmo: iTotal_LightAmmo = iValue; break;
		case ENUM_Condition.Total_HeavyAmmo: iTotal_HeavyAmmo = iValue; break;
		case ENUM_Condition.Total_Bomb: iTotal_Bomb = iValue; break;
		case ENUM_Condition.Total_Kill: iTotal_Kill = iValue; break;
		default: break;
		}//switch
	}
	// 增值
	public void Add(ENUM_Condition emCondition, int iValue)
	{
		switch(emCondition)
		{
		case ENUM_Condition.Single_Stage: iSingle_Stage += iValue; break;
		case ENUM_Condition.Single_MemberSave: iSingle_MemberSave += iValue; break;
		case ENUM_Condition.Single_MemberFire: iSingle_MemberFire += iValue; break;
		case ENUM_Condition.Total_Stage: iTotal_Stage += iValue; break;
		case ENUM_Condition.Total_Crystal: iTotal_Crystal += iValue; break;
		case ENUM_Condition.Total_Currency: iTotal_Currency += iValue; break;
		case ENUM_Condition.Total_Battery: iTotal_Battery += iValue; break;
		case ENUM_Condition.Total_LightAmmo: iTotal_LightAmmo += iValue; break;
		case ENUM_Condition.Total_HeavyAmmo: iTotal_HeavyAmmo += iValue; break;
		case ENUM_Condition.Total_Bomb: iTotal_Bomb += iValue; break;
		case ENUM_Condition.Total_Kill: iTotal_Kill += iValue; break;
		default: break;
		}//switch
	}
	// 取值
	public int Get(ENUM_Condition emCondition)
	{
		switch(emCondition)
		{
		case ENUM_Condition.Single_Stage: return iSingle_Stage;
		case ENUM_Condition.Single_MemberSave: return iSingle_MemberSave;
		case ENUM_Condition.Single_MemberFire: return iSingle_MemberFire;
		case ENUM_Condition.Total_Stage: return iTotal_Stage;
		case ENUM_Condition.Total_Crystal: return iTotal_Crystal;
		case ENUM_Condition.Total_Currency: return iTotal_Currency;
		case ENUM_Condition.Total_Battery: return iTotal_Battery;
		case ENUM_Condition.Total_LightAmmo: return iTotal_LightAmmo;
		case ENUM_Condition.Total_HeavyAmmo: return iTotal_HeavyAmmo;
		case ENUM_Condition.Total_Bomb: return iTotal_Bomb;
		case ENUM_Condition.Total_Kill: return iTotal_Kill;
		default: return 0;
		}//switch
	}
	// 取得成就是否完成
	public bool IsComplete(int iGUID, int iLevel)
	{
		string szGUID = DBFAchievement.GetGUID(iGUID, iLevel);

		return Data.ContainsKey(szGUID) ? Data[szGUID] : false;
	}
	// 清除單輪資料
	public void ClearSingle()
	{
		iSingle_Stage = 0;
		iSingle_MemberSave = 0;
		iSingle_MemberFire = 0;
	}
	// 清除累積資料
	public void ClearTotal()
	{
		iTotal_Stage = 0;
		iTotal_Crystal = 0;
		iTotal_Currency = 0;
		iTotal_Battery = 0;
		iTotal_LightAmmo = 0;
		iTotal_HeavyAmmo = 0;
		iTotal_Bomb = 0;
		iTotal_Kill = 0;
	}
	// 清除資料
	public void Clear()
	{
		Data.Clear();
		ClearSingle();
		ClearTotal();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSaveAchievement);
		PlayerPrefs.DeleteKey(GameDefine.szSaveAchievementProgress);
	}
}
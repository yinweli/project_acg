using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataRecord : MonoBehaviour
{
	static public DataRecord pthis = null;

	/* Save */
	public List<SaveRecord> Data = new List<SaveRecord>();

	/* Not Save */

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		PlayerPrefs.SetInt(GameDefine.szSaveRecordCount, Data.Count);

		for(int iPos = 0; iPos < Data.Count; ++iPos)
			PlayerPrefs.SetString(GameDefine.szSaveRecord + iPos, Json.ToString(Data[iPos]));
	}
	// 讀檔.
	public bool Load()
	{
		Data.Clear();

		if(PlayerPrefs.HasKey(GameDefine.szSaveRecordCount) == false)
			return false;

		for(int iPos = 0, iMax = PlayerPrefs.GetInt(GameDefine.szSaveRecordCount); iPos < iMax; ++iPos)
		{
			string szSave = GameDefine.szSaveRecord + iPos;
			
			if(PlayerPrefs.HasKey(szSave))
				Data.Add(Json.ToObject<SaveRecord>(PlayerPrefs.GetString(szSave)));
		}//for

		return true;
	}
	// 更新紀錄.
	public bool RecordNow()
	{
		SaveRecord Temp = new SaveRecord();
		
		Temp.iStage = DataPlayer.pthis.iStage;
		Temp.iPlayTime = DataPlayer.pthis.iPlayTime;
		Temp.iEnemyKill = DataPlayer.pthis.iEnemyKill;
		Temp.iPlayerLost = DataPlayer.pthis.iPlayerLost;
		Temp.szTime = System.DateTime.Now.ToString();
		
		Data.Add(Temp);
		
		Save();
		
		Data.Sort();
		
		int iRecCount = Data.Count;
		
		if(iRecCount > 0 && Data[iRecCount - 1].szTime == Temp.szTime)
			return true;
		
		return false;
	}
	// 清除資料
	public void Clear()
	{
		Data.Clear();
	}
}
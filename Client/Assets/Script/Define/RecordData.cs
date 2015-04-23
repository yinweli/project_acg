﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class RecordData : MonoBehaviour
{
	static public RecordData pthis = null;

	/* Save */
	public List<SaveRecord> Recordlist = new List<SaveRecord>();

	/* Not Save */

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		PlayerPrefs.SetInt(GameDefine.szSaveRecordCount, Recordlist.Count);

		for(int iPos = 0; iPos < Recordlist.Count; ++iPos)
			PlayerPrefs.SetString(GameDefine.szSaveRecord + iPos, Json.ToString(Recordlist[iPos]));
	}
	// 讀檔.
	public bool Load()
	{
		Recordlist.Clear();

		if(PlayerPrefs.HasKey(GameDefine.szSaveRecordCount) == false)
			return false;

		for(int iPos = 0, iMax = PlayerPrefs.GetInt(GameDefine.szSaveRecordCount); iPos < iMax; ++iPos)
		{
			string szSave = GameDefine.szSaveRecord + iPos;
			
			if(PlayerPrefs.HasKey(szSave))
				Recordlist.Add(Json.ToObject<SaveRecord>(PlayerPrefs.GetString(szSave)));
		}//for

		return true;
	}
	// 清除資料
	public void ClearData()
	{
		Recordlist.Clear();
	}
}
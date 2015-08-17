using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataEnemy : MonoBehaviour
{
	static public DataEnemy pthis = null;

	/* Save */
	public List<SaveEnemy> Data = new List<SaveEnemy>();

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		int iCount = 0;

		foreach(KeyValuePair<GameObject,int> Itor in SysMain.pthis.Enemy)
		{
            if (Itor.Key)
            {
                AIEnemy EnemyTemp = Itor.Key.GetComponent<AIEnemy>();

                if (EnemyTemp && EnemyTemp.iHP > 0)
                {
                    SaveEnemy Temp = new SaveEnemy();
					Vector3 Pos = EnemyPos(Itor.Key.transform.localPosition);

					Temp.iMonster = EnemyTemp.iMonster;
					Temp.iHP = EnemyTemp.iHP;
					Temp.fPosX = Pos.x;
					Temp.fPosY = Pos.y;

					PlayerPrefs.SetString(GameDefine.szSaveEnemy + iCount, Json.ToString(Temp));
					++iCount;
                }//if
            }//if
		}//for

		PlayerPrefs.SetInt(GameDefine.szSaveEnemyCount, iCount);
	}
	// 讀檔.
	public bool Load()
	{
		Data.Clear();

		if(PlayerPrefs.HasKey(GameDefine.szSaveEnemyCount) == false)
			return false;

		for(int iPos = 0, iMax = PlayerPrefs.GetInt(GameDefine.szSaveEnemyCount); iPos < iMax; ++iPos)
		{
			string szSave = GameDefine.szSaveEnemy + iPos;
			
			if(PlayerPrefs.HasKey(szSave))
				Data.Add(Json.ToObject<SaveEnemy>(PlayerPrefs.GetString(szSave)));
		}//for

		return true;
	}
	// 取得怪物位置
	public Vector3 EnemyPos(Vector3 Pos)
	{
		return new Vector3(Pos.x - CameraCtrl.pthis.gameObject.transform.localPosition.x, Pos.y - CameraCtrl.pthis.gameObject.transform.localPosition.y);
	}
	// 清除資料
	public void Clear()
	{
		Data.Clear();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSaveEnemy);
	}
}
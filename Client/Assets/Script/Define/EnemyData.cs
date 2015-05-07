using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class EnemyData : MonoBehaviour
{
	static public EnemyData pthis = null;

	public List<SaveEnemy> EnemyList = new List<SaveEnemy>();

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		int iCount = 0;

		foreach(GameObject Itor in SysMain.pthis.Enemy)
		{
            if (Itor)
            {
                AIEnemy Enemy = Itor.GetComponent<AIEnemy>();

                if (Enemy && Enemy.iHP > 0)
                {
                    SaveEnemy Data = new SaveEnemy();

					Data.iMonster = Enemy.iMonster;
					Data.iHP = Enemy.iHP;
                    Data.fPosX = Itor.transform.localPosition.x - CameraCtrl.pthis.gameObject.transform.localPosition.x;
                    Data.fPosY = Itor.transform.localPosition.y - CameraCtrl.pthis.gameObject.transform.localPosition.y;

					PlayerPrefs.SetString(GameDefine.szSaveEnemy + iCount, Json.ToString(Data));
					++iCount;
                }//if
            }//if
		}//for

		PlayerPrefs.SetInt(GameDefine.szSaveEnemyCount, iCount);
	}
	// 讀檔.
	public bool Load()
	{
		EnemyList.Clear();

		if(PlayerPrefs.HasKey(GameDefine.szSaveEnemyCount) == false)
			return false;

		for(int iPos = 0, iMax = PlayerPrefs.GetInt(GameDefine.szSaveEnemyCount); iPos < iMax; ++iPos)
		{
			string szSave = GameDefine.szSaveEnemy + iPos;
			
			if(PlayerPrefs.HasKey(szSave))
				EnemyList.Add(Json.ToObject<SaveEnemy>(PlayerPrefs.GetString(szSave)));
		}//for

		return true;
	}
	// 清除存檔
	public void Clear()
	{
		EnemyList = new List<SaveEnemy>();
		PlayerPrefs.DeleteKey(GameDefine.szSaveEnemy);
	}
}

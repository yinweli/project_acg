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
		List<SaveEnemy> Data = new List<SaveEnemy>();

		foreach(GameObject Itor in SysMain.pthis.Enemy)
		{
			AIEnemy Enemy = Itor.GetComponent<AIEnemy>();

			if(Enemy && Enemy.iHP > 0)
			{
				SaveEnemy Temp = new SaveEnemy();

				Temp.iMonster = Enemy.iMonster;
				Temp.iHP = Enemy.iHP;
				Temp.fMoveSpeed = Enemy.fMoveSpeed;
				Temp.iThreat = Enemy.iThreat;
				Temp.fPosX = Itor.transform.localPosition.x;
				Temp.fPosY = Itor.transform.localPosition.y;

				Data.Add(Temp);
			}//if
		}//for
		
		PlayerPrefs.SetString(GameDefine.szSaveEnemy, Json.ToString(Data));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveEnemy) == false)
			return false;
		
		EnemyList = Json.ToObject<List<SaveEnemy>>(PlayerPrefs.GetString(GameDefine.szSaveEnemy));

		return EnemyList != null;
	}
}

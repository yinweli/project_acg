﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*  關卡有關卡編號、怪物間隔、怪物能量這三個屬性
 * 關卡編號
    表示這是第幾個關卡，同時也表示這個關卡的難度。
 * 怪物間隔
    每波怪物產生的間隔。
    每波怪物出現後就重新計算怪物間隔。
    怪物間隔為隨機值，最短為２０秒，最長為４０秒。
 * 怪物能量
    每波怪物產生的數量與種類的計算根據。
    能量基礎值為１００。
    能量增加值為１０。
    怪物能量 = 能量基礎值 + 關卡編號 / ５ * 能量增加值。
*/

public class EnemyCreater : MonoBehaviour
{
    public static EnemyCreater pthis = null;

    public GameObject CameraCtrl;
    // 波數能量.
    public int iEnegry = 0;
    // 本關敵人與機率列表<敵人名稱,機率>.
    public Dictionary<string, int> StageEnemy = new Dictionary<string, int>();
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StartNew()
    {
        // 計算總波數能量：怪物能量 = 能量基礎值 + 關卡編號 / 每多少關增加難度 * 能量增加值。.
        iEnegry = GameDefine.iBaseEngry + (PlayerData.pthis.iStage / GameDefine.iStageCount * GameDefine.iWeightEngry);

        StartCoroutine(Creater());
    }
    // ------------------------------------------------------------------
    // 關卡敵人過濾.
    void GetStageEnemy()
    {
        StageEnemy.Clear();
        // 從DBF表中過濾該出現的怪物加入.
        //SysMain.pthis.iStage

        // 目前暫時一種怪.
        StageEnemy.Add(EnemyType.Enemy_001.ToString(), 0);
        // 平均給予機率.        
        foreach (KeyValuePair<string, int> itor in StageEnemy)
            StageEnemy[itor.Key] = StageEnemy.Count * 10;
    }
    // ------------------------------------------------------------------
    // 敵人佇列產生函式.
    public void ListEnemyCreater(List<string> EnemyList)
    {
        // 先清理List.
        EnemyList.Clear();

		if (SysMain.pthis.bIsGaming == false)
			return;

        // 取得可使用的怪物列表.
        List<int> pEnemy = Rule.MonsterList(PlayerData.pthis.iStage);
        
        int iTempEnegry = iEnegry;
		string szReport = "";
		
		while (iTempEnegry > 0)
		{
			int iEnemy = LibCSNStandard.Tool.RandomPick(pEnemy);
			DBFMonster DBFData = GameDBF.This.GetMonster(iEnemy) as DBFMonster;
			
			if (DBFData == null)
			{
				Debug.Log("DBFMonster(" + iEnemy + ") null");
				return;
			}//if
			
			if (iTempEnegry > 0 && iTempEnegry <= 1)
			{
				iTempEnegry -= 1;
				EnemyList.Add(string.Format("Enemy_{0:000}", 1));
				szReport += string.Format("({0}, {1}) ", 1, 1);
			}

			if (iTempEnegry > 0 && iTempEnegry >= DBFData.Enegry)
			{
				iTempEnegry -= DBFData.Enegry;
				EnemyList.Add(string.Format("Enemy_{0:000}", iEnemy));
				szReport += string.Format("({0}, {1}) ", iEnemy, DBFData.Enegry);
			}
		}
		
		Debug.Log("enegry " + iEnegry + " " + szReport);
    }
    // ------------------------------------------------------------------
    // 偕同程序
    IEnumerator Creater()
    {
        while (SysMain.pthis.bIsGaming)
        {
            // 計算要出的敵人佇列.
            List<string> ListEnemy = new List<string>();
            ListEnemyCreater(ListEnemy);

            // 計算等待間隔.
            yield return new WaitForSeconds(Random.Range(GameDefine.iMINWaitSec, GameDefine.iMAXWaitSec));
            
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                switch (Random.Range(1, 4))
                {
                    case 1: //上方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i], 
                            CameraCtrl.transform.localPosition.x + Random.Range(-500.0f, 500.0f), 
                            CameraCtrl.transform.localPosition.y + Random.Range(380.0f, 450.0f));
                         break;
                    case 2: //左方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i],
                            CameraCtrl.transform.localPosition.x + Random.Range(-470.0f, -520.0f),
                            CameraCtrl.transform.localPosition.y + +Random.Range(-300.0f, 400.0f));
                        break;
                    case 3: //右方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i],
                            CameraCtrl.transform.localPosition.x + Random.Range(470.0f, 520.0f),
                            CameraCtrl.transform.localPosition.y + +Random.Range(-300.0f, 400.0f));
                        break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }        
    }
    // ------------------------------------------------------------------
    public bool CheckPos(GameObject pObj)
    {
        if (Vector2.Distance(CameraCtrl.transform.position, pObj.transform.position) > 2.8f)
            return true;
        else
            return false;
    }

}

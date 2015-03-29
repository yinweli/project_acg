﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCreater : MonoBehaviour
{
    //public static EnemyCreater pthis = null;
    // 波數能量.
    public int iEnegry = 0;
    // 本關敵人與機率列表<敵人名稱,機率>.
    public Dictionary<string, int> StageEnemy = new Dictionary<string, int>();
    void Awake()
    {
        //pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        StartNew();
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StartNew()
    {
        // 計算總波數能量.
        iEnegry = SysMain.pthis.iStage * GameDefine.iWeightEngry + GameDefine.iBaseEngry;

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

        // 測試用資料先.
        EnemyList.Add("Enemy_001");
        EnemyList.Add("Enemy_001");
        EnemyList.Add("Enemy_001");
        EnemyList.Add("Enemy_001");
        EnemyList.Add("Enemy_001");

        EnemyList.Add("Enemy_001");
        EnemyList.Add("Enemy_001");
    }
    // ------------------------------------------------------------------
    // 偕同程序
    IEnumerator Creater()
    {
        while (SysMain.pthis.bIsGaming)
        {
            // 計算等待間隔.
            yield return new WaitForSeconds(Random.Range(GameDefine.iMINWaitSec, GameDefine.iMAXWaitSec));
            // 計算要出的敵人佇列.
            List<string> ListEnemy = new List<string>();
            ListEnemyCreater(ListEnemy);

            for (int i = 0; i < ListEnemy.Count; i++)
            {
                switch (Random.Range(1, 4))
                {
                    case 1: //上方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i], Random.Range(-500.0f, 500.0f), Mathf.Abs(transform.parent.localPosition.y) + Random.Range(375.0f, 430.0f));
                        break;
                    case 2: //左方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i], Random.Range(-470.0f, -520.0f), Mathf.Abs(transform.parent.localPosition.y) + Random.Range(-395.0f, 395.0f));
                        break;
                    case 3: //右方.
                        UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i], Random.Range(470.0f, 520.0f), Mathf.Abs(transform.parent.localPosition.y) + Random.Range(-395.0f, 395.0f));
                        break;
                }                
            }
            //上方出敵.
            //pObj = CreateUI(Random.Range(-500.0f, 500.0f), Mathf.Abs(transform.parent.localPosition.y) + 400);
        }        
    }
    // ------------------------------------------------------------------
}
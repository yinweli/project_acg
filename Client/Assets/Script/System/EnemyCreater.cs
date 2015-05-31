using UnityEngine;
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
    public Shader pShader;

    public GameObject CameraCtrl;
    // 波數能量.
    public int iEnegry = 0;

    int iCount = 0;
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StartNew()
    {
        iCount = 0;
        // 計算總波數能量：怪物能量 = (能量基礎值 + 關卡編號 * 能量增加值) * 額外能量加成
		iEnegry = (int)((GameDefine.iBaseEngry + (int)(PlayerData.pthis.iStage * GameDefine.fUpgradeEnegry)) * (1.0f + Rule.FeatureF(ENUM_ModeFeature.AddEnegry)));

        StartCoroutine(Creater());
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StopCreate()
    {
        StopAllCoroutines();
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
        List<int> pEnemy = Rule.MonsterList();
        
        int iTempEnegry = iEnegry;
		
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
				EnemyList.Add(string.Format("Enemy/{0:000}", 1));
			}

			if (iTempEnegry > 0 && iTempEnegry >= DBFData.Enegry)
			{
				iTempEnegry -= DBFData.Enegry;
				EnemyList.Add(string.Format("Enemy/{0:000}", iEnemy));
			}
		}
    }
    // ------------------------------------------------------------------
    // 復原怪物.
    public void CreateOldEnemy()
    {
        foreach (SaveEnemy itor in EnemyData.pthis.EnemyList)
        {
            GameObject pEnemy = UITool.pthis.CreateUIByPos(gameObject, string.Format("Enemy/{0:000}", itor.iMonster), itor.fPosX + CameraCtrl.transform.localPosition.x, itor.fPosY + CameraCtrl.transform.localPosition.y);
            
            if (pEnemy && pEnemy.GetComponent<AIEnemy>())
            {
                ToolKit.SetLayer(iCount, pEnemy.GetComponentsInChildren<UI2DSprite>());
                pEnemy.GetComponent<AIEnemy>().iHP = itor.iHP;      
            }
            iCount++;
        }        
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
			yield return new WaitForSeconds(Random.Range(GameDefine.iMinWaitSec, GameDefine.iMaxWaitSec));
            
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                GameObject pObj = null;
                switch (Random.Range(1, 4))
                {
                    case 1: //上方.
                        pObj = UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i], 
                            CameraCtrl.transform.localPosition.x + Random.Range(-500.0f, 500.0f), 
                            CameraCtrl.transform.localPosition.y + Random.Range(380.0f, 450.0f));
                         break;
                    case 2: //左方.
                         pObj = UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i],
                            CameraCtrl.transform.localPosition.x + Random.Range(-470.0f, -520.0f),
                            CameraCtrl.transform.localPosition.y + Random.Range(-300.0f, 400.0f));
                        break;
                    case 3: //右方.
                        pObj = UITool.pthis.CreateUIByPos(gameObject, ListEnemy[i],
                            CameraCtrl.transform.localPosition.x + Random.Range(470.0f, 520.0f),
                            CameraCtrl.transform.localPosition.y + Random.Range(-300.0f, 400.0f));
                        break;
                }
                ToolKit.SetShader(pShader, pObj.GetComponentsInChildren<UI2DSprite>());
                ToolKit.SetLayer(iCount, pObj.GetComponentsInChildren<UI2DSprite>());
                iCount++;
                yield return new WaitForSeconds(0.05f);
            }
        }        
    }
    // ------------------------------------------------------------------
    public bool CheckPos(GameObject pObj)
    {
        if (Vector2.Distance(CameraCtrl.transform.position, pObj.transform.position) > 1.95f)
            return true;
        else
            return false;
    }

}

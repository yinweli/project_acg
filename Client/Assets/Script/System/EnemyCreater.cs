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

    public GameObject ObjCamCtrl;
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

        // 每5關為魔王關
		Debug.Log(Rule.AppearBossStage() ? "Boss Stage" : "Normal Stage");

		if(Rule.AppearBossStage())
            StartCoroutine(BossCreater());
        else
        {
            // 計算總波數能量：怪物能量 = (能量基礎值 + 關卡編號 * 能量增加值) * 額外能量加成
            iEnegry = (int)((GameDefine.iBaseEngry + (int)(DataPlayer.pthis.iStage * GameDefine.fUpgradeEnegry)) * (1.0f + Rule.FeatureF(ENUM_ModeFeature.AddEnegry)));

            StartCoroutine(Creater());
        }
    }
    // ------------------------------------------------------------------
    // 開始新的關卡.
    public void StopCreate()
    {
        StopAllCoroutines();
    }
    // ------------------------------------------------------------------
    // 敵人佇列產生函式.
    public void ListEnemyCreater(List<int> EnemyList)
    {
        // 先清理List.
        EnemyList.Clear();

		if (SysMain.pthis.bIsGaming == false)
			return;

        // 取得可使用的怪物列表.
        List<int> pEnemy = Rule.MonsterList();
        
        int iTempEnegry = iEnegry;
		bool bNoMove = false;
		
		while (iTempEnegry > 0)
		{
			int iEnemy = LibCSNStandard.Tool.RandomPick(pEnemy);
			DBFMonster DBFData = GameDBF.pthis.GetMonster(iEnemy) as DBFMonster;
			
			if (DBFData == null)
			{
				Debug.Log("DBFMonster(" + iEnemy + ") null");
				return;
			}//if

			// 只產生一隻擋路怪
			if((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.NoMove)
			{
				if(bNoMove == false)
					bNoMove = true;
				else 
					continue;
			}//if

			if (iTempEnegry > 0 && iTempEnegry <= 1)
			{
				iTempEnegry -= 1;
				EnemyList.Add(1);
			}

			if (iTempEnegry > 0 && iTempEnegry >= DBFData.Enegry)
			{
				iTempEnegry -= DBFData.Enegry;
				EnemyList.Add(iEnemy);
			}
		}
    }
    // ------------------------------------------------------------------
    // 產生一隻怪物.
    public GameObject CreateOneEnemy(int iMonster, int iHP, float fPosX, float fPosY)
    {
        GameObject pEnemy = UITool.pthis.CreateUIByPos(gameObject, string.Format("Enemy/{0:000}", iMonster), fPosX, fPosY);

        if (pEnemy && pEnemy.GetComponent<AIEnemy>())
        {
            ToolKit.SetLayer(iCount, pEnemy.GetComponentsInChildren<SpriteRenderer>());
            pEnemy.GetComponent<AIEnemy>().iMonster = iMonster;
            pEnemy.GetComponent<AIEnemy>().iHP = iHP;
        }
        iCount++;

        return pEnemy;
    }
    // ------------------------------------------------------------------
    // 復原怪物.
    public void CreateOldEnemy()
    {
        foreach (SaveEnemy itor in DataEnemy.pthis.Data)
            CreateOneEnemy(itor.iMonster, itor.iHP, itor.fPosX + ObjCamCtrl.transform.localPosition.x, itor.fPosY + ObjCamCtrl.transform.localPosition.y);   
    }
    // ------------------------------------------------------------------
    IEnumerator BossCreater()
    {
        yield return new WaitForSeconds(Random.Range(GameDefine.iMinWaitSec, GameDefine.iMaxWaitSec));
        while (SysMain.pthis.bIsGaming)
        {
            // 魔王還在不出怪.
            if (SysMain.pthis.Enemy.Count > 0)
                yield return new WaitForSeconds(0.1f);
            else
            {
                // 等待1.8秒後出新魔王.
                yield return new WaitForSeconds(1.8f);

				int iIndex = DataPlayer.pthis.iStage / GameDefine.iBossStage % GameDefine.iBossCount;

				if(iIndex == 0)
					iIndex = GameDefine.iBossCount;

                CreateOneEnemy(iIndex + 1000, -1, ObjCamCtrl.transform.localPosition.x + Random.Range(-500.0f, 500.0f), ObjCamCtrl.transform.localPosition.y + Random.Range(380.0f, 450.0f));               
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
    // ------------------------------------------------------------------
    // 偕同程序
    IEnumerator Creater()
    {
        while (SysMain.pthis.bIsGaming)
        {
            // 計算要出的敵人佇列.
            List<int> ListEnemy = new List<int>();
            ListEnemyCreater(ListEnemy);

            // 計算等待間隔.
			yield return new WaitForSeconds(Random.Range(GameDefine.iMinWaitSec, GameDefine.iMaxWaitSec));            
            for (int i = 0; i < ListEnemy.Count; i++)
            {
                DBFMonster DBFData = GameDBF.pthis.GetMonster(ListEnemy[i]) as DBFMonster;
                // 如果是擋路怪要生在路上.
                if ((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.NoMove)
                    CreateByRoad(ListEnemy[i]);
                // 魅惑怪需要生在路附近.
                else if ((ENUM_ModeMonster)DBFData.Mode == ENUM_ModeMonster.Bewitch)
                    CreateByStandOutRoad(ListEnemy[i]);
                else
                    CreateByNormal(ListEnemy[i]);
                yield return new WaitForSeconds(0.05f);
            }
        }        
    }
    // ------------------------------------------------------------------
    void CreateByNormal(int iMonster)
    {
        Vector2 pCamPos = ObjCamCtrl.transform.localPosition;
        switch (Random.Range(1, 4))
        {
            case 1: //上方.
                CreateOneEnemy(iMonster, -1, pCamPos.x + Random.Range(-500.0f, 500.0f), pCamPos.y + Random.Range(380.0f, 450.0f));
                break;
            case 2: //左方.
                CreateOneEnemy(iMonster, -1, pCamPos.x + Random.Range(-470.0f, -520.0f), pCamPos.y + Random.Range(-300.0f, 400.0f));
                break;
            case 3: //右方.
                CreateOneEnemy(iMonster, -1, pCamPos.x + Random.Range(470.0f, 520.0f), pCamPos.y + Random.Range(-300.0f, 400.0f));
                break;
        }
    }
    // ------------------------------------------------------------------
    void CreateByRoad(int iMonster)
    {
        int iRoad = CameraCtrl.pthis.iNextRoad + Random.Range(7, 15);

        if (iRoad < DataMap.pthis.DataRoad.Count)
        {
            GameObject pObjMon = CreateOneEnemy(iMonster, -1, 0, 0);
            pObjMon.transform.position = GetRoadPos(iRoad);
        }
    }
    // ------------------------------------------------------------------
    void CreateByStandOutRoad(int iMonster)
    {
        int iRoad = CameraCtrl.pthis.iNextRoad + Random.Range(7, 15);
        if (DataMap.pthis.DataRoad.Count > iRoad)
        {
            GameObject pObjMon = CreateOneEnemy(iMonster, -1, 0, 0);
            pObjMon.transform.position = GetRoadPos(iRoad);

            int iRand = Random.Range(3, 11);
            float fPosX = 0;
            if (iRand < 7)
                fPosX = pObjMon.transform.localPosition.x + (GameDefine.iBlockSize * iRand);
            else
                fPosX = pObjMon.transform.localPosition.x + (-GameDefine.iBlockSize * (iRand - 4));

            pObjMon.transform.localPosition = new Vector2(fPosX, pObjMon.transform.localPosition.y);
        }
    }
    // ------------------------------------------------------------------
    public Vector2 GetRoadPos(int iRoad)
    {

        if (MapCreater.pthis.GetRoadObj(iRoad))
            return MapCreater.pthis.GetRoadObj(iRoad).transform.position;
        else
        {
            Debug.Log("iRoad: " + iRoad + " Obj: " + MapCreater.pthis.GetRoadObj(iRoad));
            return Vector2.zero;
        }
    }
    // ------------------------------------------------------------------
    public bool CheckPos(GameObject pObj)
    {
        if (Vector2.Distance(ObjCamCtrl.transform.position, pObj.transform.position) > 1.95f)
            return true;
        else
            return false;
    }
    // ------------------------------------------------------------------
    public void SetAI(GameObject ObjMoster, ENUM_ModeMonster enumMode)
    {
        switch (enumMode)
        {
            case ENUM_ModeMonster.ActiveDark:
                ObjMoster.AddComponent<EnemyLightStop>();
                break;
            case ENUM_ModeMonster.Tied:
                ObjMoster.AddComponent<EnemyTied>();
                break;
            case ENUM_ModeMonster.NoMove:
                ObjMoster.AddComponent<EnemyLeoCat>();
                break;
            case ENUM_ModeMonster.Bewitch:
                ObjMoster.AddComponent<EnemyBewitch>();
                break;
            case ENUM_ModeMonster.Shield:
                break;
            case ENUM_ModeMonster.Boss:
                ObjMoster.AddComponent<EnemyBoss>();
                break;
            default:
                ObjMoster.AddComponent<EnemyNormal>();
                break;
        }
    }
}
